using Microsoft.JSInterop;

namespace BlazoriumWASMS.Extensions
{
    public static class IJSRuntimeExtensions
    {
        /*
        
        Η κλήση από C# μέσω IJSRuntime χρησιμοποιεί await επειδή η γέφυρα Blazor ↔ JavaScript είναι async.
        Όχι επειδή η ίδια η JS συνάρτηση είναι async.
            Στην JS πλευρά → η ShowToastr είναι sync.
            Στην Blazor/C# πλευρά → η κλήση είναι async για λόγους interop (ώστε να μη μπλοκάρει το .NET runtime).
         */
        public static async Task ToastrSuccess(this IJSRuntime js, string message)
        {
            await js.InvokeVoidAsync("ShowToastr", "success", message);
        }

        public static async Task ToastrError(this IJSRuntime js, string message)
        {
            await js.InvokeVoidAsync("ShowToastr", "error", message);
        }
    }
}
