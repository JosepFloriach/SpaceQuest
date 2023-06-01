using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ConsoleCommand
{
    string GetCommandName();
    string GetHelp();
    bool Execute(params string[] arguments);
}
