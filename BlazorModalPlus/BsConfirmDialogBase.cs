using Microsoft.AspNetCore.Components;

namespace BlazorModalPlus
{
    public abstract partial class BsConfirmDialogBase : ComponentBase
    {
        /// <summary>
        /// DarkMode (<see cref="DarkModeDialog"/>) supported since Bootstrap 5.3
        /// </summary>
        [Parameter]
        public DarkModeDialog DarkMode { get; set; } = DarkModeDialog.Light;

        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> UserAttributes { get; set; } = new Dictionary<string, object>();
        protected IEnumerable<ButtonItem>? Buttons { get; set; }
        protected string? Title { get; set; }
        protected string Message { get; set; } = string.Empty;
        protected bool Visible { get; set; } = false;

        public string BsDarkMode => Enum.GetName(typeof(DarkModeDialog), DarkMode).ToLower();

        protected MarkupString GetMsgMarkupString()
        {
            return new MarkupString(!string.IsNullOrEmpty(Message)
                ? Message
                : "Are you sure?"
                );
        }

        protected string GetBtnRender(BtnRenderStyle render)
        {
            string strRender = Enum.GetName(typeof(BtnRenderStyle), render).ToLower();

            return $"btn-{strRender}";
        }

        public virtual async Task HideDialog()
        {
            Visible = false;

            await Task.CompletedTask;
        }
    }
}
