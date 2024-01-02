Expression Engine is a supporting module I built for one of the projects at work. 

It helps to generate User Friendly Stat information for the workflow. Ex: For the Stat "OneLineSummary", the Expression Engine will go over the config file and get the corresponding action config. Then, run several criteria for that action. For the matching criteria, it will pick the corresponding expression. Run it and return back the final executed Expression.

Lets say we are currently have an ongoing Pizza Delivery Order. Currently, the status of the order is Delivery is complete. It was delivered by "John Doe". 

When we feed this workflow to Expression Engine. It will give us this. "Task Deliver Pizza completed on 01/01/2024 12:00 AM by John Doe. Overall time taken: 2 days". Please ignore the unrealistic delivery time taken. :-) 

You can run the console program and you would see this output. 

I have implemented a demo DataManager and DemoConfigManager for IDataManager & IConfigManager. In my real project, they are actual AgilePoint Workflow DataManager which would interact with the real AgilePoint Server to get the requested workflow/task related information.

The Expression Engine is made of the following

Interfaces and its implementations:
1. IExpression
    - interface members, methods
        - Evaluate: Takes IExpressionInterpreter & IActionParams. Returns evaluated expression.
        - ExpressionResult: Result of this expression.
        - Successor: Next expression in the Expression Chain.
        - PredecessorResult: Result of the previous expression in the Expression Chain.
        - TargetExpression: Expression to evaluate if the Criteria matches. Of type IExpression.
        - Criteria: Criteria to evaluate. Of type ICriteria.
    - inteface implementations
        - ExpressionChain
        - AllCondition
        - AnyCondition
        - BooleanExpression
        - CriteriaExpression
        - MultiCriteria
        - MultiExpression
        - SimpleExpression  
2. ICriteria
    - TargetCriteria: Criteria to evaluate. Of type string.
    - Evaluate: Takes IExpressionInterpreter & IActionParams. Returns evaluated expression.
3. IMacro
    - Name: Macro Name
    - Params: Dictionary of workflow contextual information such as Task Name, Process Name, Task Created Date, User, etc.
    - MacroExpression: Passed in macro expression in raw string. The macro can be inside criteria, expression or anything. Its just a way to get the information from the process/task such as passed in task status, process status, etc.

Runnable modules that uses above interfaces:
1. ExpressionBuilder
2. ExpressionInterpreter
3. MacroProcessor
