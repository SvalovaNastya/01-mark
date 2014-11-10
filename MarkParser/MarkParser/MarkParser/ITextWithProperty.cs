using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MarkToHtml
{
    public interface ITextWithProperty
    {
        string Text { get; set; }

        string ToHtmlString();
    }
}
