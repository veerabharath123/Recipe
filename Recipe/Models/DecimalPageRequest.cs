using Recipe.Helpers;

namespace Recipe.Dtos.RequestDto
{
    public class DecimalPageRequest : DecimalRequest
    {
        public PagerRequest pager { get; set; } = new PagerRequest();
    }
}
