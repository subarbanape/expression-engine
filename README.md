Expression Engine is a supporting module I built for one of the projects at work. 

It helps to generate User Friendly Stat information for the workflow. Ex: For the Stat "OneLineSummary", the Expression Engine will go over the config file and get the corresponding action config. Then, run several criteria for that action. For the matching criteria, it will pick the corresponding expression. Run it and return back the final executed Expression.

Lets say we are currently have an ongoing Pizza Delivery Order. Currently, the status of the order is Delivery is complete. It was delivered by "John Doe". 

When we feed this workflow to Expression Engine. It will give us this. "Task Deliver Pizza completed on 01/01/2024 12:00 AM by John Doe. Overall time taken: 2 days". Please ignore the unrealistic delivery time taken. :-) 

You can run the console program and you would see this output. 

I have implemented a demo DataManager and DemoConfigManager for IDataManager & IConfigManager. In my real project, they are actual AgilePoint Workflow DataManager which would interact with the real AgilePoint Server to get the requested workflow/task related information.

The Expression Engine is made of the following

Interfaces and its implementations:
1. IExpression
    - one of the core elements that makes criteria and expression. for any expression to execute, criteria needs to be evaluated to True.
    - most of the time, an expression is made of expression tree/chain. each expression is passed to expression interpreter to evaulate the expression.
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
3. ICriteria
    - TargetCriteria: Criteria to evaluate. Of type string.
    - Evaluate: Takes IExpressionInterpreter & IActionParams. Returns evaluated expression.
4. IMacro
    - Name: Macro Name
    - Params: Dictionary of workflow contextual information such as Task Name, Process Name, Task Created Date, User, etc.
    - MacroExpression: Passed in macro expression in raw string. The macro can be inside criteria, expression or anything. Its just a way to get the information from the process/task such as passed in task status, process status, etc.

Runnable modules that uses above interfaces:
1. MacroProcessor
   - The low level module which interacts with AgilePoint via Data Manager.
   - Brings back requested information such as Task Status, Task Duration, Open Tasks etc.
   - Just one and only implementation of IMacroProcessor
2. ExpressionBuilder
    - Extension to prepare a syntactic expression chain for actions.
    - Check out my program.cs to see how I use this to build a sample expression chain for the demo.
4. ExpressionInterpreter
    - Just one and only implementation of IExpressionInterpreter
    - Has 2 methods. Both are Interpret.
    - One of them takes IExpression as the param. And the other takes ICriteria as the param.
    - It has further implemetations of helper module IExpressionParser.
          - IExpressionParser further parses the instructions/conditions and helps decide to evaluate the overall instruction.
          - ex: "MACRO(GetTaskStatus, {TaskName}, {RequestId}) is Cancelled or Removed". The expression parser has several implementations which will help the overall instruction to evaluate to True/False.
          - Implementations of IExpressionParser
              - AuxiliaryVerbParser: Gets the values present in-between 'or'. ex: alfredo or parmesan or mozzarella, returns: [alfredo, parmesan, mozzarella]    
              - TasksMacroParamsExpressionParser
              - MacroParamsExpressionParser
              - MacroExpressionParser
              - ValuesToCompareExpressionParser: 
