﻿using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MvcContrib.TestHelper;
using NUnit.Framework;
using RestfulRouting;
using RestfulRouting.Tests;
using Rhino.Mocks;
using NUnit.Should;

namespace FormHelperSpecs
{
	public abstract class base_context : route_test_context
	{
		protected HttpRequestBase _httpRequestBase;
		protected RequestContext _requestContext;
		protected RouteData _routeData;
		protected FormHelper _formHelper;
		protected string _form;

		protected override void given()
		{
			base.given();

			var builder = new TestControllerBuilder();
			var context = new RequestContext(builder.HttpContext, new RouteData());
			context.HttpContext.Response.Stub(x => x.ApplyAppPathModifier(null)).IgnoreArguments().Do(new Func<string, string>(s => s)).Repeat.Any();

			var helper = new UrlHelper(context);

			_formHelper = new FormHelper(helper);
		}
	}

	namespace when_a_blogs_resource_has_been_mapped
	{
		public abstract class base_context : FormHelperSpecs.base_context
		{
			protected override void given()
			{
				base.given();
				
				_map.Resources<BlogsController>();
			}
		}

		[TestFixture]
		public class when_generating_a_form_for_a_new_blog_entity : base_context
		{
			protected override void when()
			{
				_form = _formHelper.FormFor(new Blog());
			}

			[Test]
			public void should_generate_form_for_create_action()
			{
				_form.ShouldBe("<form method=\"post\" action=\"/blogs\">");
			}
		}

		[TestFixture]
		public class when_generating_a_form_for_an_existing_blog_entity : base_context
		{
			protected override void when()
			{
				_form = _formHelper.FormFor(new Blog{ Id = 2 });
			}

			[Test]
			public void should_generate_form_for_update_action()
			{
				_form.Contains("<form method=\"post\" action=\"/blogs/2\">").ShouldBeTrue();
			}

			[Test]
			public void should_generate_override_for_put_method()
			{
				_form.Contains("<input type=\"hidden\" name=\"_method\" value=\"put\" />").ShouldBeTrue();
			}
		}

		[TestFixture]
		public class when_generating_a_delete_form_for_an_existing_blog_entity : base_context
		{
			protected override void when()
			{
				_form = _formHelper.DeleteFormFor(new Blog { Id = 2 });
			}

			[Test]
			public void should_generate_form_for_update_action()
			{
				_form.Contains("<form method=\"post\" action=\"/blogs/2\">").ShouldBeTrue();
			}

			[Test]
			public void should_generate_override_for_delete_method()
			{
				_form.Contains("<input type=\"hidden\" name=\"_method\" value=\"delete\" />").ShouldBeTrue();
			}
		}

		namespace when_a_blogs_resource_has_been_mapped_with_a_nested_posts_resource
		{
			public abstract class base_context : FormHelperSpecs.base_context
			{
				protected override void given()
				{
					base.given();

					_map.Resources<BlogsController>(x => x.Resources<PostsController>());
				}
			}

			[TestFixture]
			public class when_generating_a_form_for_a_new_post_entity : base_context
			{
				protected override void when()
				{
					_form = _formHelper.FormFor(new Post(), new Blog { Id = 2 });
				}

				[Test]
				public void should_generate_form_for_update_action()
				{
					_form.Contains("<form method=\"post\" action=\"/blogs/2/posts\">").ShouldBeTrue();
				}
			}

			[TestFixture]
			public class when_generating_a_form_for_an_existing_post_entity : base_context
			{
				protected override void when()
				{
					_form = _formHelper.FormFor(new Post { Id = 3 }, new Blog { Id = 2 });
				}

				[Test]
				public void should_generate_form_for_update_action()
				{
					_form.Contains("<form method=\"post\" action=\"/blogs/2/posts/3\">").ShouldBeTrue();
				}

				[Test]
				public void should_generate_override_for_put_method()
				{
					_form.Contains("<input type=\"hidden\" name=\"_method\" value=\"put\" />").ShouldBeTrue();
				}
			}

			[TestFixture]
			public class when_generating_a_delete_form_for_an_existing_post_entity : base_context
			{
				protected override void when()
				{
					_form = _formHelper.DeleteFormFor(new Post { Id = 3 }, new Blog { Id = 2 });
				}

				[Test]
				public void should_generate_form_for_update_action()
				{
					_form.Contains("<form method=\"post\" action=\"/blogs/2/posts/3\">").ShouldBeTrue();
				}

				[Test]
				public void should_generate_override_for_delete_method()
				{
					_form.Contains("<input type=\"hidden\" name=\"_method\" value=\"delete\" />").ShouldBeTrue();
				}
			}

			namespace when_a_blogs_resource_has_been_mapped_with_a_nested_posts_resource_with_a_nested_comments_resource
			{
				public abstract class base_context : FormHelperSpecs.base_context
				{
					protected override void given()
					{
						base.given();

						_map.Resources<BlogsController>(x => x.Resources<PostsController>(m => m.Resources<CommentsController>()));
					}
				}

				[TestFixture]
				public class when_generating_a_form_for_a_new_comment_entity : base_context
				{
					protected override void when()
					{
						_form = _formHelper.FormFor(new Comment(), new Post{ Id = 3 }, new Blog { Id = 2 });
					}

					[Test]
					public void should_generate_form_for_update_action()
					{
						_form.Contains("<form method=\"post\" action=\"/blogs/2/posts/3/comments\">").ShouldBeTrue();
					}
				}

				[TestFixture]
				public class when_generating_a_form_for_an_existing_comment_entity : base_context
				{
					protected override void when()
					{
						_form = _formHelper.FormFor(new Comment{ Id = 4 }, new Post { Id = 3 }, new Blog { Id = 2 });
					}

					[Test]
					public void should_generate_form_for_update_action()
					{
						_form.Contains("<form method=\"post\" action=\"/blogs/2/posts/3/comments/4\">").ShouldBeTrue();
					}

					[Test]
					public void should_generate_override_for_put_method()
					{
						_form.Contains("<input type=\"hidden\" name=\"_method\" value=\"put\" />").ShouldBeTrue();
					}
				}

				[TestFixture]
				public class when_generating_a_delete_form_for_an_existing_comment_entity : base_context
				{
					protected override void when()
					{
						_form = _formHelper.DeleteFormFor(new Comment { Id = 4 }, new Post { Id = 3 }, new Blog { Id = 2 });
					}

					[Test]
					public void should_generate_form_for_update_action()
					{
						_form.Contains("<form method=\"post\" action=\"/blogs/2/posts/3/comments/4\">").ShouldBeTrue();
					}

					[Test]
					public void should_generate_override_for_delete_method()
					{
						_form.Contains("<input type=\"hidden\" name=\"_method\" value=\"delete\" />").ShouldBeTrue();
					}
				}
			}
		}
	}
}
