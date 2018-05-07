using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Code;

namespace Web {
    public partial class BlogPost : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            int? id = GetBlogPostId();
            var blogPost = BlogPostsProvider.GetBlogPost(id);
            if(blogPost == null)
                Response.Redirect("BlogTimeline.aspx");
            Page.Title = Subject.InnerText = blogPost.Subject;
            Date.Text = blogPost.Date.ToString("ddd, MMM dd yyyy");
            Body.InnerText = blogPost.Body;
            BlogImage.Src = blogPost.ImageUrl;
        }
        int? GetBlogPostId() {
            int id;
            var strId = Request.Params["id"];
            return int.TryParse(strId, out id) ? id : (int?)null;
        }
    }
}