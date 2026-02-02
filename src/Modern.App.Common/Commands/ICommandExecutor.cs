namespace Modern.App.Common.Commands;

public interface ICommandExecutor
{
    Task Execute(ICommand command);
    Task<TResult> Execute<TResult>(ICommand<TResult> command);
}