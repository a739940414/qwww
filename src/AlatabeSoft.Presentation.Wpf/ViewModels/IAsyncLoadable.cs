using System.Threading;
using System.Threading.Tasks;

namespace AlatabeSoft.Presentation.Wpf.ViewModels;

public interface IAsyncLoadable
{
    Task LoadAsync(CancellationToken cancellationToken);
}
