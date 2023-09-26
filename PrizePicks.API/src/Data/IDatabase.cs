using PrizePicks.API.Models;

namespace PrizePicks.API.Data;

public interface IDatabase
{
    public Task<IEnumerable<ICage>> CagesAsync();
    public void RemoveAsync(ICage instance);
    public void UpdateAsync(ICage instance);

    public Task<IEnumerable<IDinosaur>> DinosaursAsync();
    public void RemoveAsync(IDinosaur instance);
    public void UpdateAsync(IDinosaur instance);
}
