using Logitar.Faktur.Contracts.Search;

namespace Logitar.Faktur.Contracts.Departments;

public interface IDepartmentService
{
  Task<Department?> ReadAsync(string storeId, string departmentNumber, CancellationToken cancellationToken = default);
  Task<CommandResult> RemoveAsync(string storeId, string departmentNumber, CancellationToken cancellationToken = default);
  Task<CommandResult> SaveAsync(string storeId, string departmentNumber, SaveDepartmentPayload payload, CancellationToken cancellationToken = default);
  Task<SearchResults<Department>> SearchAsync(string storeId, SearchDepartmentsPayload payload, CancellationToken cancellationToken = default);
  Task<CommandResult> UpdateAsync(string storeId, string departmentNumber, UpdateDepartmentPayload payload, CancellationToken cancellationToken = default);
}
