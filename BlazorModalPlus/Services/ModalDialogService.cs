using Microsoft.Extensions.Logging;

namespace BlazorModalPlus.Services
{
    /// <summary>
    /// Service for managing modal dialogs programmatically
    /// </summary>
    public class ModalDialogService : IModalDialogService
    {
        private readonly ILogger<ModalDialogService>? _logger;
        private BsSimpleConfirmDialog? _currentDialog;
        private TaskCompletionSource<DialogButtonResult>? _currentTaskCompletionSource;

        public ModalDialogService(ILogger<ModalDialogService>? logger = null)
        {
            _logger = logger;
        }

        /// <summary>
        /// Sets the dialog instance to use for displaying modals
        /// </summary>
        /// <param name="dialog">The dialog instance</param>
        public void SetDialogInstance(BsSimpleConfirmDialog dialog)
        {
            _currentDialog = dialog;
        }

        /// <summary>
        /// Shows a confirmation dialog with the specified parameters
        /// </summary>
        /// <param name="message">The message to display</param>
        /// <param name="title">The title of the dialog</param>
        /// <param name="buttons">The button configuration</param>
        /// <returns>The dialog result</returns>
        public async Task<DialogButtonResult> ShowConfirmationAsync(string message, string? title = null, DialogButtons buttons = DialogButtons.OkCancel)
        {
            if (_currentDialog == null)
            {
                _logger?.LogWarning("No dialog instance is available. Make sure to call SetDialogInstance first.");
                return DialogButtonResult.Cancel;
            }

            try
            {
                _currentTaskCompletionSource = new TaskCompletionSource<DialogButtonResult>();
                
                // Set up callback to capture result
                _currentDialog.ConfirmCallback = Microsoft.AspNetCore.Components.EventCallback.Factory
                    .Create<DialogButtonResult>(this, OnDialogResult);

                await _currentDialog.ShowDialog(message, title, buttons);
                
                return await _currentTaskCompletionSource.Task;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error showing confirmation dialog");
                return DialogButtonResult.Cancel;
            }
        }

        /// <summary>
        /// Shows an information dialog with the specified message
        /// </summary>
        /// <param name="message">The message to display</param>
        /// <param name="title">The title of the dialog</param>
        /// <returns>A task representing the asynchronous operation</returns>
        public async Task ShowInfoAsync(string message, string? title = null)
        {
            await ShowConfirmationAsync(message, title, DialogButtons.Ok);
        }

        private void OnDialogResult(DialogButtonResult result)
        {
            _currentTaskCompletionSource?.SetResult(result);
            _currentTaskCompletionSource = null;
        }
    }
}