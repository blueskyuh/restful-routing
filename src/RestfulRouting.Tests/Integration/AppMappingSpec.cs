using Machine.Specifications;
using RestfulRouting.Tests.Integration.Behaviours;
using RestfulRouting.Tests.Integration.Contexts;

namespace RestfulRouting.Tests.Integration
{
    public class BlogArea : RestfulRoutingArea
    {
        public BlogArea()
        {
            Area<BlogsController>("", () =>
            {
                Resources<BlogsController>();
                Resources<PostsController>();
            });
        }
    }

    public class when_mapping_an_unnamed_app : base_context
    {
        public class WebsiteApp : RestfulRoutingArea
        {
            public WebsiteApp()
            {
                App<BlogArea>("");
            }
        }

        Because of = () => new WebsiteApp().RegisterRoutes(routes);

        Behaves_like<BlogsResources> a_blogs_resource;

        Behaves_like<PostsResources> a_posts_resource;
    }

    public class when_mapping_a_named_app : base_context
    {
        public class WebsiteApp : RestfulRoutingArea
        {
            public WebsiteApp()
            {
                App<BlogArea>("admin/");
            }
        }

        Because of = () => new WebsiteApp().RegisterRoutes(routes);

        Behaves_like<AdminBlogsResources> a_blogs_resource;
    }
}