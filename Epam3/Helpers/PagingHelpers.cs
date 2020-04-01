using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;
using Epam3.Models;

namespace Epam3.Helpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html,
        PageInfo pageInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pageInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                // если текущая страница, то выделяем ее,
                // например, добавляя класс
                if (i == pageInfo.PageNumber)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }
                tag.AddCssClass("btn btn-default");
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }



        public static MvcHtmlString Sort(this HtmlHelper html, List<FacultyViewModel> list)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder tbody = new TagBuilder("tbody");
            for (int i = 0; i < list.Count-1; i++)
            {
                TagBuilder tr = new TagBuilder("tr");
                tr.InnerHtml = $"<td>{list.ElementAt(i).Name}</td>";
                tr.InnerHtml += $"<td>{list.ElementAt(i).QtyBudget}</td>";
                tr.InnerHtml += $"<td>{list.ElementAt(i).QtyAll}</td>";
                tr.InnerHtml += "<td><button type = \"button\" class=\"btn btn-primary\" onclick=\"location.href = \'@Url.Action(\"AmdinApplicant\", \"Home\", new { faculty = item.Id, page=1 })\'\">Абитуриенты</button></td>";
                tr.InnerHtml += "<td><button type = \"button\" class=\"btn btn-warning\" onclick=\"location.href = \'@Url.Action(\"EditFaculty\", \"Home\", new { faculty = item.Id })\'\">Редактировать</button></td>";
                tr.InnerHtml += "<td><button type = \"button\" class=\"btn btn-danger\" onclick=\"location.href = \'@Url.Action(\"DeleteFaculty\", \"Home\", new { id = item.Id })\'\">Удалить</button></td>";
                tbody.InnerHtml += tr;
            }
            result.Append(tbody.ToString());
            return MvcHtmlString.Create(result.ToString());
        }
    }
}