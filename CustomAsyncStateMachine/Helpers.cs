using System.Linq;
using System.Threading.Tasks;

namespace CusomtAsyncStateMachine
{
    public class Helpers
    {
        public Task<Foo[]> ProcessAsync(UsersDTO data)
            => Task.FromResult(data.Data
                .Select(x => new Foo() { Value = x.Id + 1 })
                .ToArray());
    }
}
