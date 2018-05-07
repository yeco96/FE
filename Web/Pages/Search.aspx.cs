using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Code;

namespace Web {
    public partial class Search : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            ((Layout)Page.Master).ShowSearch = false;
            var query = Session["query"] as string;
            if(!string.IsNullOrEmpty(query)) {
                Session["query"] = null;
                SearchGridView.SearchPanelFilter = query;
            }
            SearchGridView.DataSource = BlogPostsProvider.GetBlogPosts();
            SearchGridView.DataBind();
        }
    }
}