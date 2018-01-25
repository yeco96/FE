using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Code;

namespace Web.UserControls {
    public partial class BlogNavigation : System.Web.UI.UserControl {
        protected void Page_Load(object sender, EventArgs e) {
            PrepareNavBar();
        }
        void PrepareNavBar() {
            var blogTimelineUrl = "~/Pages/BlogTimeline.aspx";
            BlogCategoriesNavBar.Groups.Add("Archive (All)").NavigateUrl = blogTimelineUrl;
            foreach(var yearsGroup in BlogPostsProvider.GetBlogsByCategories()) {
                var navBarGroup = BlogCategoriesNavBar.Groups.Add(string.Format("{0} ({1})", yearsGroup.Key.ToString(), yearsGroup.Value.Sum(b => b.Value.Count())));
                navBarGroup.NavigateUrl = string.Format("{0}?year={1}", blogTimelineUrl, yearsGroup.Key);
                foreach(var monthGroup in yearsGroup.Value) {
                    var blogs = monthGroup.Value;
                    var navBarItem = navBarGroup.Items.Add();
                    var month = blogs.First().Date.ToString("MMMM");
                    navBarItem.Text = string.Format("{0} ({1})", month, blogs.Count());
                    navBarItem.NavigateUrl = string.Format("{0}?year={1}&month={2}", blogTimelineUrl, yearsGroup.Key, monthGroup.Key);
                }
            }
        }
    }
}