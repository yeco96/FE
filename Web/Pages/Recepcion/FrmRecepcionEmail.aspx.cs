using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Net.NetworkInformation;
using System.Net.Security;
using System.Net.Sockets;
using EAGetMail; //add EAGetMail namespace

namespace Web.Pages.Recepcion
{
    public partial class FrmRecepcionEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //Está pintando todos los correos
            //ExtraerCorreos();
            extraerGmail();
            
        }


        private void extraerGmail()
        {
            // Create a folder named "inbox" under current directory
            // to save the email retrieved.
            string curpath = Directory.GetCurrentDirectory();
            string mailbox = String.Format("{0}\\inbox", curpath);

            // If the folder is not existed, create it.
            if (!Directory.Exists(mailbox))
            {
                Directory.CreateDirectory(mailbox);
            }

            // Gmail IMAP4 server is "imap.gmail.com"
            MailServer oServer = new MailServer("imap.gmail.com",
                        "roswel030@gmail.com", "Soporte93", ServerProtocol.Imap4);
            MailClient oClient = new MailClient("TryIt");

            // Set SSL connection,
            oServer.SSLConnection = true;

            // Set 993 IMAP4 port
            oServer.Port = 993;

            try
            {
                oClient.Connect(oServer);
                MailInfo[] infos = oClient.GetMailInfos();
                for (int i = 0; i < infos.Length; i++)
                {
                    MailInfo info = infos[i];
                    Console.WriteLine("Index: {0}; Size: {1}; UIDL: {2}",
                        info.Index, info.Size, info.UIDL);

                    // Download email from GMail IMAP4 server
                    Mail oMail = oClient.GetMail(info);

                    Console.WriteLine("From: {0}", oMail.From.ToString());
                    Console.WriteLine("Subject: {0}\r\n", oMail.Subject);
                    TextBox1.Text = "From:" + oMail.From.ToString();
                    TextBox2.Text = "Subject:" + oMail.Subject;

                    // Generate an email file name based on date time.
                    System.DateTime d = System.DateTime.Now;
                    System.Globalization.CultureInfo cur = new
                        System.Globalization.CultureInfo("en-US");
                    string sdate = d.ToString("yyyyMMddHHmmss", cur);
                    string fileName = String.Format("{0}\\{1}{2}{3}.eml",
                        mailbox, sdate, d.Millisecond.ToString("d3"), i);

                    // Save email to local disk
                    oMail.SaveAs(fileName, true);

                    // Mark email as deleted in GMail account.
                    oClient.Delete(info);
                }

                // Quit and purge emails marked as deleted from Gmail IMAP4 server.
                oClient.Quit();
            }
            catch (Exception ep)
            {
                Console.WriteLine(ep.Message);
            }
        }
        
        //private void ExtraerDatos_2()
        //{
        //    Imap client = new Imap();
        //    // connect to server

        //    client.Connect("imap.gmail.com", 993, SslMode.Implicit);

        //    // authenticate
        //    client.Login("username", "password");

        //    // select folder
        //    client.SelectFolder("Inbox");

        //    int NoOfEmailsPerPage = 10;
        //    int totalEmails = client.CurrentFolder.TotalMessageCount;
        //    // get message list - envelope headers
        //    ImapMessageCollection messages = client.GetMessageList(ImapListFields.Envelope);

        //    // display info about each message


        //    foreach (ImapMessageInfo message in messages)
        //    {

        //        TableCell noCell = new TableCell();

        //        noCell.CssClass = "emails-table-cell";

        //        noCell.Text = Convert.ToString(message.To);
        //        TableCell fromCell = new TableCell();
        //        fromCell.CssClass = "emails-table-cell";
        //        fromCell.Text = Convert.ToString(message.From);
        //        TableCell subjectCell = new TableCell();
        //        subjectCell.CssClass = "emails-table-cell";
        //        subjectCell.Style["width"] = "300px";
        //        subjectCell.Text = Convert.ToString(message.Subject);
        //        TableCell dateCell = new TableCell();
        //        dateCell.CssClass = "emails-table-cell";
        //        if (message.Date.OriginalTime != DateTime.MinValue)
        //            dateCell.Text = message.Date.OriginalTime.ToString();
        //        TableRow emailRow = new TableRow();
        //        emailRow.Cells.Add(noCell);
        //        emailRow.Cells.Add(fromCell);
        //        emailRow.Cells.Add(subjectCell);
        //        emailRow.Cells.Add(dateCell);
        //        EmailsTable.Rows.AddAt(2 + 0, emailRow);

        //    }
        //    int totalPages;
        //    int mod = totalEmails % NoOfEmailsPerPage;
        //    if (mod == 0)
        //        totalPages = totalEmails / NoOfEmailsPerPage;
        //    else
        //        totalPages = ((totalEmails - mod) / NoOfEmailsPerPage) + 1;
        //}

        private void ExtraerCorreos()
        {
            try

            {

                TcpClient tcpclient = new TcpClient(); // create an instance of TcpClient

                tcpclient.Connect("pop.gmail.com", 995); // HOST NAME POP SERVER and gmail uses port number 995 for POP 

                System.Net.Security.SslStream sslstream = new SslStream(tcpclient.GetStream()); // This is Secure Stream // opened the connection between client and POP Server

                sslstream.AuthenticateAsClient("pop.gmail.com"); // authenticate as client 

                //bool flag = sslstream.IsAuthenticated; // check flag 

                System.IO.StreamWriter sw = new StreamWriter(sslstream); // Asssigned the writer to stream

                System.IO.StreamReader reader = new StreamReader(sslstream); // Assigned reader to stream

                sw.WriteLine("USER roswel030@gmail.com"); // refer POP rfc command, there very few around 6-9 command

                sw.Flush(); // sent to server

                sw.WriteLine("PASS Soporte93");
                sw.Flush();

                sw.WriteLine("RETR 1"); // this will retrive your first email 

                sw.Flush();

                sw.WriteLine("Quit "); // close the connection

                sw.Flush();

                string str = string.Empty;
                string strTemp = string.Empty;

                while ((strTemp = reader.ReadLine()) != null)
                {

                    if (strTemp == ".") // find the . character in line

                    {

                        break;
                    }

                    if (strTemp.IndexOf("-ERR") != -1)
                    {

                        break;
                    }

                    str += strTemp;

                }

                Response.Write(str);

                Response.Write("<BR>" + "Congratulation.. ....!!! You read your first gmail email ");
            }

            catch (Exception ex)
            {

                Response.Write(ex.Message);

            }
        }
    }
}
