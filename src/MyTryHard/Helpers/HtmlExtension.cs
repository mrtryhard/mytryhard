using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using MyTryHard.Models;
using System;
using System.Collections.Generic;

namespace MyTryHard.Helpers
{
    public static class HtmlExtension
    {
        public static IHtmlContent DropDownListForCategories(this HtmlHelper helper, List<Category> list)
        {
            return DropDownListForCategories(helper, list, Guid.Empty);
        }

        public static IHtmlContent DropDownListForCategories(this IHtmlHelper helper, List<Category> list, Guid selected)
        {        
            ViewDataDictionary vdd = new ViewDataDictionary(helper.ViewContext.ViewData);
            vdd.Add("Selected", selected);

            return helper.Partial("HtmlHelper/CategoriesList", list, vdd);
        }
    }
}
