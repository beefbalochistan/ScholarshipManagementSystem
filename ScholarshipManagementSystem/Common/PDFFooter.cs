using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Common
{
    public partial class Footer : PdfPageEventHelper

    {

        public override void OnEndPage(PdfWriter writer, Document doc)

        {

            Paragraph footer = new Paragraph("System generated report on "+ String.Format("{0:ddd, MMM d, yyyy}", DateTime.Today) +" BEEF", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, iTextSharp.text.Font.NORMAL));

            footer.Alignment = Element.ALIGN_LEFT;

            PdfPTable footerTbl = new PdfPTable(1);

            footerTbl.TotalWidth = 300;

            footerTbl.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell cell = new PdfPCell(footer);

            cell.Border = 0;

            cell.PaddingLeft = 10;

            footerTbl.AddCell(cell);

            footerTbl.WriteSelectedRows(0, -1, 315, 30, writer.DirectContent);

        }

    }
}
