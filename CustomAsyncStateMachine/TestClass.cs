using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace CusomtAsyncStateMachine
{
    public class TestClass
    {
        public async Task<Foo[]> ReadFoosAsync1(
            string filename)
        {
            using var fileReader = new StreamReader(filename);
            var jsonText = await fileReader.ReadToEndAsync();
            var data = JsonSerializer.Deserialize<UsersDTO>(jsonText);
            var foos = await _helpers.ProcessAsync(data);
            return foos;
        }

        public Task<Foo[]> ReadFoosAsync2(
            string filename)
        {
            var stateMachine = new ReadFoosAsync2_StateMachine(this, filename);

            stateMachine.MoveNext();

            return stateMachine.ReturnValue;
        }

        private readonly Helpers _helpers
            = new Helpers();

        private struct ReadFoosAsync2_StateMachine
        {
            public ReadFoosAsync2_StateMachine(
                TestClass @this,
                string filename)
            {
                _this = @this;
                _returnValueSource = new TaskCompletionSource<Foo[]>();

                _state = -1;
                _lastAwait = null;

                _filename = filename;
                _fileReader = null;
            }

            public Task<Foo[]> ReturnValue
                => _returnValueSource.Task;

            public void MoveNext()
            {
                ++_state;
                try
                {
                    switch (_state)
                    {
                        case 0:
                            _fileReader = new StreamReader(_filename);
                            _lastAwait = _fileReader.ReadToEndAsync();
                            break;

                        case 1:
                            var jsonText = ((Task<string>)_lastAwait!).Result;
                            var data = JsonSerializer.Deserialize<UsersDTO>(jsonText);
                            _lastAwait = _this._helpers.ProcessAsync(data);
                            break;

                        default:
                            var foos = ((Task<Foo[]>)_lastAwait!).Result;
                            _returnValueSource.SetResult(foos);
                            return;
                    }

                    if (_lastAwait.IsCompletedSuccessfully)
                        MoveNext();
                    else if (_lastAwait.IsFaulted)
                        _returnValueSource.SetException(_lastAwait.Exception!.InnerException!);
                    else if (_lastAwait.IsCanceled)
                        _returnValueSource.SetCanceled();
                    else
                    {
                        var @this = this;
                        _lastAwait.ContinueWith(_ => @this.MoveNext());
                    }
                }
                catch (Exception ex)
                {
                    _returnValueSource.SetException(ex);
                }
                finally
                {
                    if (_returnValueSource.Task.IsCompleted)
                        _fileReader?.Dispose();
                }
            }

            private readonly TestClass _this;
            private TaskCompletionSource<Foo[]> _returnValueSource;

            private int _state;
            private Task? _lastAwait;

            private string _filename;
            private StreamReader? _fileReader;
        }
    }
}
