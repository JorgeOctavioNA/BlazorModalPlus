using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorModalPlus
{
    public abstract partial class BsConfirmDialogBase : ComponentBase, IDisposable
    {
        private bool _disposed = false;

        /// <summary>
        /// DarkMode (<see cref="DarkModeDialog"/>) supported since Bootstrap 5.3
        /// </summary>
        [Parameter]
        public DarkModeDialog DarkMode { get; set; } = DarkModeDialog.Light;

        /// <summary>
        /// Event fired when the dialog is shown
        /// </summary>
        [Parameter]
        public EventCallback OnShow { get; set; }

        /// <summary>
        /// Event fired when the dialog is hidden
        /// </summary>
        [Parameter]
        public EventCallback OnHide { get; set; }

        /// <summary>
        /// Allow closing the dialog with Escape key
        /// </summary>
        [Parameter]
        public bool CloseOnEscape { get; set; } = true;

        /// <summary>
        /// Allow closing the dialog by clicking outside of it
        /// </summary>
        [Parameter]
        public bool CloseOnBackdropClick { get; set; } = true;

        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> UserAttributes { get; set; } = new Dictionary<string, object>();
        
        protected IEnumerable<ButtonItem>? Buttons { get; set; }
        protected string? Title { get; set; }
        protected string Message { get; set; } = string.Empty;
        protected bool Visible { get; set; } = false;

        protected string BsDarkMode => Enum.GetName(typeof(DarkModeDialog), DarkMode)?.ToLower() ?? "light";

        protected MarkupString GetMsgMarkupString()
        {
            return new MarkupString(!string.IsNullOrEmpty(Message) ? Message : string.Empty);
        }

        protected string GetBtnRender(BtnRenderStyle render)
        {
            string strRender = Enum.GetName(typeof(BtnRenderStyle), render)?.ToLower() ?? "primary";
            return $"btn-{strRender}";
        }

        /// <summary>
        /// Handles keyboard events for the dialog
        /// </summary>
        protected async Task OnKeyDown(KeyboardEventArgs e)
        {
            if (e.Key == "Escape" && CloseOnEscape && Visible)
            {
                await HideDialog();
            }
        }

        /// <summary>
        /// Handles backdrop clicks
        /// </summary>
        protected async Task OnBackdropClick(MouseEventArgs e)
        {
            // Only close if the click was on the modal backdrop (outer div), not on the dialog content
            if (CloseOnBackdropClick && Visible)
            {
                await HideDialog();
            }
        }

        /// <summary>
        /// Validates message parameter
        /// </summary>
        protected static void ValidateMessage(string? message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException("Message cannot be null or empty.", nameof(message));
            }
        }

        public virtual async Task HideDialog()
        {
            if (!Visible) return;

            Visible = false;
            
            if (OnHide.HasDelegate)
            {
                await OnHide.InvokeAsync();
            }
            
            StateHasChanged();
        }

        protected virtual async Task ShowDialogInternal()
        {
            if (Visible) return;

            Visible = true;
            
            if (OnShow.HasDelegate)
            {
                await OnShow.InvokeAsync();
            }
            
            StateHasChanged();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                // Cleanup resources if needed
                _disposed = true;
            }
        }
    }
}
