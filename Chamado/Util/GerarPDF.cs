//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using iTextSharp.tool.xml;
//using iTextSharp.tool.xml.html;
//using iTextSharp.tool.xml.html.table;
//using iTextSharp.tool.xml.parser;
//using iTextSharp.tool.xml.pipeline.css;
//using iTextSharp.tool.xml.pipeline.end;
//using iTextSharp.tool.xml.pipeline.html;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Web.Mvc;

//namespace Util
//{
//    public static class GerarPDF
//    {
//        public static string RenderPartialViewToString(Controller vController, string vViewName, object vModel)
//        {
//            vController.ViewData.Model = vModel;
//            using (StringWriter vStringWriter = new StringWriter())
//            {
//                ViewEngineResult vViewResult = ViewEngines.Engines.FindPartialView(vController.ControllerContext, vViewName);
//                ViewContext vViewContext = new ViewContext(vController.ControllerContext, vViewResult.View, vController.ViewData, vController.TempData, vStringWriter);
//                vViewResult.View.Render(vViewContext, vStringWriter);

//                return vStringWriter.ToString();
//            }
//        }

//        public static byte[] GerarPDFArquivo(StringReader vStringReader, bool vPaisagem = false)
//        {
//            byte[] vBytes;
//            string vCss = "";

//            using (MemoryStream vMemoryStream = new MemoryStream())
//            {
//                using (var vPdfDoc = new Document((vPaisagem ? PageSize.A4.Rotate() : PageSize.A4), 10f, 10f, 10f, 0f))
//                {
//                    PdfWriter vWriter = PdfWriter.GetInstance(vPdfDoc, vMemoryStream);
//                    vPdfDoc.Open();

//                    ITagProcessorFactory vTagProcessorFactory = Tags.GetHtmlTagProcessorFactory();
//                    vTagProcessorFactory.AddProcessor(new TableDataProcessor(), new string[] { HTML.Tag.TD });

//                    HtmlPipelineContext vHtmlPipelineContext = new HtmlPipelineContext(null);
//                    vHtmlPipelineContext.SetTagFactory(vTagProcessorFactory);

//                    PdfWriterPipeline vPdfWriterPipeline = new PdfWriterPipeline(vPdfDoc, vWriter);
//                    HtmlPipeline vHtmlPipeline = new HtmlPipeline(vHtmlPipelineContext, vPdfWriterPipeline);

//                    ICSSResolver vCssResolver = XMLWorkerHelper.GetInstance().GetDefaultCssResolver(true);
//                    vCssResolver.AddCss(vCss, "utf-8", true);

//                    CssResolverPipeline vCssResolverPipeline = new CssResolverPipeline(vCssResolver, vHtmlPipeline);

//                    XMLWorker vWorker = new XMLWorker(vCssResolverPipeline, true);
//                    XMLParser vParser = new XMLParser(vWorker);

//                    vParser.Parse(vStringReader);
//                }
//                vBytes = vMemoryStream.ToArray();
//                vMemoryStream.Close();
//            }

//            return vBytes;
//        }
//    }

//    public class TableDataProcessor : TableData
//    {
//        bool HasWritingMode(IDictionary<string, string> attributeMap)
//        {
//            bool hasStyle = attributeMap.ContainsKey("style");
//            return hasStyle
//                    && attributeMap["style"].Split(new char[] { ';' })
//                    .Where(x => x.StartsWith("writing-mode:"))
//                    .Count() > 0
//                ? true : false;
//        }

//        public override IList<IElement> End(IWorkerContext ctx, Tag tag, IList<IElement> currentContent)
//        {
//            var cells = base.End(ctx, tag, currentContent);
//            var attributeMap = tag.Attributes;
//            if (HasWritingMode(attributeMap))
//            {
//                var pdfPCell = (PdfPCell)cells[0];
//                pdfPCell.Rotation = 90;
//            }
//            return cells;
//        }
//    }
//}