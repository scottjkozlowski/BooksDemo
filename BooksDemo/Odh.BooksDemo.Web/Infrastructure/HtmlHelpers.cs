using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Odh.BooksDemo.Web.Infrastructure
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString ActionImage(this HtmlHelper html, string action, object routeValues, string imagePath, string alt)
        {
            var url = new UrlHelper(html.ViewContext.RequestContext);

            // build the <img> tag
            var imgBuilder = new TagBuilder("img");
            imgBuilder.MergeAttribute("src", url.Content(imagePath));
            imgBuilder.MergeAttribute("alt", alt);
            var imgHtml = imgBuilder.ToString(TagRenderMode.SelfClosing);

            // build the <a> tag
            var anchorBuilder = new TagBuilder("a");
            anchorBuilder.MergeAttribute("href", url.Action(action, routeValues));
            anchorBuilder.InnerHtml = imgHtml; // include the <img> tag inside
            var anchorHtml = anchorBuilder.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(anchorHtml);
        }

        public static MvcHtmlString ActionQueryLink(this HtmlHelper htmlHelper,
             string linkText, string action, object routeValues)
        {
            var newRoute = routeValues == null
                 ? htmlHelper.ViewContext.RouteData.Values
                 : new RouteValueDictionary(routeValues);

            newRoute = htmlHelper.ViewContext.HttpContext.Request.QueryString
                 .ToRouteDic(newRoute);

            return HtmlHelper.GenerateLink(htmlHelper.ViewContext.RequestContext,
                 htmlHelper.RouteCollection, linkText, null,
                 action, null, newRoute, null).ToMvcHtml();
        }

        public static MvcHtmlString CheckBoxList(this HtmlHelper helper, IEnumerable<SelectListItem> items,
            string name, int columns, object htmlAttributes)
        {
            //Build a string builder
            var output = new StringBuilder();
            output.Append("<div>");
            output.Append("<table><tr>");
            var itemCount = 0;

            //Loop through items and generate checkbox list.
            foreach (var item in items)
            {
                itemCount++;
                output.Append("<td style='white-space:normal;'>");
                var checkboxList = new TagBuilder("input");
                checkboxList.MergeAttribute("type", "checkbox");
                checkboxList.MergeAttribute("name", name, true);
                checkboxList.MergeAttribute("ID", name + "-" + item.Value, true);
                checkboxList.MergeAttribute("value", item.Value);

                if (!ReferenceEquals(htmlAttributes, null))
                {
                    checkboxList.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
                }

                // Check to see if it's checked                    
                if (item.Selected)
                {
                    checkboxList.MergeAttribute("checked", "checked");
                }

                checkboxList.SetInnerText(item.Text);
                output.Append(checkboxList.ToString(TagRenderMode.SelfClosing));
                output.Append("&nbsp;");
                output.Append(item.Text);

                //List alignment as per number of columns specified
                if ((itemCount % columns) == 0)
                {
                    output.Append("</td>");
                    output.Append("</tr>");
                    output.Append("<tr>");
                }
                else
                {
                    output.Append("</td>");
                }
            }

            output.Append("</tr></table>");
            output.Append("</div>");

            return MvcHtmlString.Create(output.ToString());
        }

        public static MvcHtmlString DisplayFileName(this HtmlHelper helper, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return new MvcHtmlString("&nbp;");
            }

            var extension = (value.Contains(".") ? value.Substring(value.LastIndexOf(".")) : string.Empty);
            var fnm = value.Substring(0, value.Length - extension.Length);
            var fileGuidIndex = fnm.LastIndexOf("_");
            var fileGuid = fileGuidIndex < 0 ? string.Empty : fnm.Substring(fileGuidIndex);
            var origFnm = string.Format("{0}{1}", fnm.Substring(0, fnm.Length - fileGuid.Length), extension);
            origFnm = origFnm.Replace("_", " ");

            return new MvcHtmlString(origFnm);
        }

        public static MvcHtmlString DisplayFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, bool flag = true)
        {
            object o = expression.Compile().Invoke(html.ViewData.Model);
            if (o.GetType() == typeof(bool))
            {
                if ((bool)o)
                {
                    return new MvcHtmlString("Yes");
                }

                return new MvcHtmlString("No");
            }

            return DisplayFor(html, expression);
        }

        public static MvcHtmlString EnumDescriptionDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var enumType = GetNonNullableModelType(metadata);
            var values = Enum.GetValues(enumType).Cast<TEnum>();

            var converter = TypeDescriptor.GetConverter(enumType);


            var items =
                 from value in values
                 select new SelectListItem
                 {
                     Text = GetEnumDescription(enumType, value.ToString()),//converter.ConvertToString(value),
                     Value = value.ToString(),
                     Selected = value.Equals(metadata.Model)
                 };

            if (metadata.IsNullableValueType)
            {
                items = SingleEmptyItem.Concat(items);
            }

            return htmlHelper.DropDownListFor(
                 expression,
                 items
                 );
        }

        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var enumType = GetNonNullableModelType(metadata);
            var values = Enum.GetValues(enumType).Cast<TEnum>();

            var converter = TypeDescriptor.GetConverter(enumType);


            var items =
                 from value in values
                 select new SelectListItem
                 {
                     Text = converter.ConvertToString(value),
                     Value = value.ToString(),
                     Selected = value.Equals(metadata.Model)
                 };

            if (metadata.IsNullableValueType)
            {
                items = SingleEmptyItem.Concat(items);
            }

            return htmlHelper.DropDownListFor(
                 expression,
                 items
                 );
        }

        public static string GetCurrentTheme(this HtmlHelper html)
        {
            return html.ViewContext.HttpContext.Request.QueryString["theme"] ?? "windows7";
        }

        /// <summary>
        /// Retrieve the description on the enum, e.g.
        /// [Description("Bright Pink")]
        /// BrightPink = 2,
        /// Then when you pass in the enum, it will retrieve the description
        /// </summary>
        /// <param name="en">The Enumeration</param>
        /// <returns>A string representing the friendly name</returns>
        public static string GetEnumDescription(Type enumType, string enumValue)
        {
            //Type type = en.GetType();

            var memInfo = enumType.GetMember(enumValue);

            if (memInfo != null && memInfo.Length > 0)
            {
                var attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return enumValue;
        }

        private static Type GetNonNullableModelType(ModelMetadata modelMetadata)
        {
            var realModelType = modelMetadata.ModelType;

            var underlyingType = Nullable.GetUnderlyingType(realModelType);
            if (underlyingType != null)
            {
                realModelType = underlyingType;
            }

            return realModelType;
        }

        public static T GetValueFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum)
            {
                throw new InvalidOperationException();
            }

            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

                if (attribute == null)
                {
                    if (field.Name == description)
                    {
                        return (T)field.GetValue(null);
                    }
                }
                else
                {
                    if (attribute.Description == description)
                    {
                        return (T)field.GetValue(null);
                    }
                }
            }

            throw new ArgumentException("Not found.", "description");
            // or return default(T);
        }

        // RLM - 04/14/14 - Added ImageFor HTML helper
        public static MvcHtmlString ImageFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return ImageFor(htmlHelper, expression, null);
        }
        public static MvcHtmlString ImageFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            var imgUrl = expression.Compile()(htmlHelper.ViewData.Model);
            return BuildImageTag(imgUrl.ToString(), htmlAttributes);
        }

        private static MvcHtmlString BuildImageTag(string imgUrl, object htmlAttributes)
        {
            var tag = new TagBuilder("img");

            tag.Attributes.Add("src", imgUrl);
            if (htmlAttributes != null)
            {
                tag.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            }

            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }

        // RLM - 08/15/13 - Added LabelForRequired HTML helper
        [SuppressMessage("Microsoft.Design",
            "CA1006:DoNotNestGenericTypesInMemberSignatures",
            Justification = "This is an appropriate nesting of generic types")]
        public static MvcHtmlString LabelForRequired<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string labelText = "")
        {
            return LabelHelper(html, ModelMetadata.FromLambdaExpression(expression, html.ViewData), ExpressionHelper.GetExpressionText(expression), labelText);
        }

        private static MvcHtmlString LabelHelper(HtmlHelper html, ModelMetadata metadata, string htmlFieldName, string labelText)
        {
            if (string.IsNullOrEmpty(labelText))
            {
                labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            }

            if (string.IsNullOrEmpty(labelText))
            {
                return MvcHtmlString.Empty;
            }

            var isRequired = false;

            if (metadata.ContainerType != null)
            {
                isRequired = metadata.IsRequired;
                //isRequired = metadata.ContainerType.GetProperty(metadata.PropertyName)
                //	.GetCustomAttributes(typeof(RequiredAttribute), false)
                //	.Length == 1;
            }

            var tag = new TagBuilder("label");
            tag.Attributes.Add("for", TagBuilder.CreateSanitizedId(html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(htmlFieldName)));

            if (isRequired)
            {
                tag.Attributes.Add("class", "label-required");
            }

            tag.SetInnerText(labelText);
            var output = tag.ToString(TagRenderMode.Normal);

            //if (isRequired)
            //{
            //	var asteriskTag = new TagBuilder("span");
            //	asteriskTag.Attributes.Add("class", "required");
            //	asteriskTag.SetInnerText("*");
            //	output += asteriskTag.ToString(TagRenderMode.Normal);
            //}

            return MvcHtmlString.Create(output);
        }

        //public static MvcHtmlString LinkedList(this HtmlHelper helper, IEnumerable<SiteLink> items, 
        //	string name, int columns, object htmlAttributes)
        //{
        //	//Build a string builder
        //	var output = new StringBuilder();
        //	output.Append("<div>");
        //	output.Append("<ul>");
        //	int itemCount = 0;

        //	//Look through items and generate linked list
        //	foreach (var item in items)
        //	{
        //		itemCount++;
        //		var linkedList = new TagBuilder("li");
        //		linkedList.MergeAttribute("name", name, true);
        //		linkedList.MergeAttribute("ID", name + "-" + item.SiteLinkOrder, true);
        //		linkedList.MergeAttribute("href", item.SiteLinkUrl);
        //		linkedList.SetInnerText(item.SiteLinkText);

        //		if (!ReferenceEquals(htmlAttributes, null))
        //		{
        //			linkedList.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        //		}

        //		output.Append(linkedList.ToString(TagRenderMode.SelfClosing));
        //	}

        //	output.Append("</ul>");
        //	output.Append("</div>");

        //	return MvcHtmlString.Create(output.ToString());
        //}

        public static MvcHtmlString MenuItem(this HtmlHelper helper,
             string linkText, string actionName, string controllerName, string userName = null)
        {
            var currentControllerName = (string)helper.ViewContext.RouteData.Values["controller"];
            var currentActionName = (string)helper.ViewContext.RouteData.Values["action"];

            if (linkText == "Logout" && userName != null)
            {
                linkText = string.Format("{0} (Logout)", userName);
            }

            var builder = new TagBuilder("li");
            if (currentControllerName.Equals(controllerName, StringComparison.CurrentCultureIgnoreCase)
                && currentActionName.Equals(actionName, StringComparison.CurrentCultureIgnoreCase))
            {
                builder.AddCssClass("selected");
            }

            builder.InnerHtml = helper.ActionLink(linkText, actionName, controllerName, null, new { @class = "menuItem" }).ToHtmlString();

            return MvcHtmlString.Create(builder.ToString(TagRenderMode.Normal));
        }

        public static IHtmlString RenderScripts(this HtmlHelper htmlHelper)
        {
            var scripts = htmlHelper.ViewContext.HttpContext.Items["Scripts"] as IList<string>;
            if (scripts != null)
            {
                var builder = new StringBuilder();
                foreach (var script in scripts)
                {
                    builder.AppendLine(script);
                }

                return new MvcHtmlString(builder.ToString());
            }

            return null;
        }

        private static readonly SelectListItem[] SingleEmptyItem = new[] { new SelectListItem { Text = "", Value = "" } };

        public static MvcHtmlString ToMvcHtml(this string content)
        {
            return MvcHtmlString.Create(content);
        }

        public static RouteValueDictionary ToRouteDic(this NameValueCollection collection)
        {
            return collection.ToRouteDic(new RouteValueDictionary());
        }

        public static RouteValueDictionary ToRouteDic(this NameValueCollection collection,
             RouteValueDictionary routeDic)
        {
            foreach (string key in collection.Keys)
            {
                if (!routeDic.ContainsKey(key))
                {
                    routeDic.Add(key, collection[key]);
                }
            }

            return routeDic;
        }



        /// <summary>
        /// Returns true if a specific controller action exists and
        /// the user has the ability to access it.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <returns></returns>
        public static bool HasActionPermission(this HtmlHelper htmlHelper, string actionName, string controllerName)
        {

            ControllerBase controllerBase = string.IsNullOrEmpty(controllerName) ? htmlHelper.ViewContext.Controller : GetControllerByName(htmlHelper, controllerName);
            ControllerContext controllerContext = new ControllerContext(htmlHelper.ViewContext.RequestContext, controllerBase);
            ControllerDescriptor controllerDescriptor = new ReflectedControllerDescriptor(controllerContext.Controller.GetType());
            ActionDescriptor actionDescriptor = controllerDescriptor.FindAction(controllerContext, actionName);

            if (actionDescriptor == null)
                return false;

            FilterInfo filters = new FilterInfo(FilterProviders.Providers.GetFilters(controllerContext, actionDescriptor));

            AuthorizationContext authorizationContext = new AuthorizationContext(controllerContext, actionDescriptor);
            foreach (IAuthorizationFilter authorizationFilter in filters.AuthorizationFilters)
            {
                authorizationFilter.OnAuthorization(authorizationContext);
                if (authorizationContext.Result != null)
                    return false;
            }
            return true;
        }
        private static ControllerBase GetControllerByName(HtmlHelper helper, string controllerName)
        {
            // Instantiate the controller and call Execute
            IControllerFactory factory = ControllerBuilder.Current.GetControllerFactory();

            IController controller = factory.CreateController(helper.ViewContext.RequestContext, controllerName);

            if (controller == null)
            {
                throw new InvalidOperationException(

                    String.Format(
                        CultureInfo.CurrentUICulture,
                        "Controller factory {0} controller {1} returned null",
                        factory.GetType(),
                        controllerName));

            }

            return (ControllerBase)controller;
        }
    }

}