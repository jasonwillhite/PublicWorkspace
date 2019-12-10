using System.Web.Mvc;
namespace USAbleLife.OrderManagement.Web.Models
{
    public class AjaxResult : ActionResult
    {
        public object Value { get; set; }

        public bool Success { get; set; }

        public string Error { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            
        }
    }
}