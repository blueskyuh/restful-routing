using System.Web.Mvc;
using Machine.Specifications;
using MvcContrib.TestHelper;
using RestfulRouting.Tests.Integration.Contexts;

namespace RestfulRouting.Tests.Integration.Resources
{
	public class when_mapping_a_collection_action : base_context
	{
		public class BlogArea : RestfulRoutingArea
		{
			public BlogArea()
			{
				Resources<BlogsController>(() => Collection("latest", HttpVerbs.Get));
			}
		}

		Because of = () => new BlogArea().RegisterRoutes(routes);

		It should_map_get_latest = () => "~/blogs/latest".WithMethod(HttpVerbs.Get).ShouldMapTo<BlogsController>(x => x.Latest());
	}
}