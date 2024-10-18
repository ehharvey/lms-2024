interface ICommand
{
    /// <summary>
    /// Gets the help text for the command.
    /// Should include the command name, description, and a list of valid verbs.
    /// </summary>
    string GetHelp();

    /// <summary>
    /// Gets the help text for a specific verb of the command.
    /// Should include details on how to use the verb and what it does.
    /// </summary>
    string GetHelp(Verb verb);

    /// <summary>
    /// Executes the command with the given verb.
    /// </summary>
    /// <param name="verb">The verb to execute.</param>
    /// <exception cref="ArgumentException">Thrown when the verb is invalid.</exception>
    /// <exception cref="NotImplementedException">Thrown when the verb is not implemented.</exception>
    /// <exception cref="Exception">Thrown when an error occurs during execution.</exception>
    // void Execute(Verb verb);

    /// <summary>
    /// Executes the command with the given verb.
    /// </summary>
    /// <param name="verb">The verb to execute.</param>
    /// <param name="args">The arguments for the command.</param>
    /// <exception cref="ArgumentException">Thrown when the verb is invalid.</exception>
    /// <exception cref="NotImplementedException">Thrown when the verb is not implemented.</exception>
    /// <exception cref="Exception">Thrown when an error occurs during execution.</exception>
    void Execute(Verb verb, string[] args);
}