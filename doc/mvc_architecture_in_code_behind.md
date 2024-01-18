## MVC architecture in CodeBehind

# Simple definition of MVC

MVC is a design pattern that consists of three parts: model, view, and controller. View is the display part. Dynamic data models are placed in the view. Controllers are responsible for determining the view and model for requests.

Using the MVC Design Pattern In most MVC frameworks, controllers must be configured in the root routes. In this structure, the request reaches the route and the route recognizes the controller based on the text patterns and then calls the controller. The configuration of the controller is in the path of a poor process and the wrong structure, which is placed at the beginning of the request and response cycle and causes problems for that structure.

In the CodeBehind framework, the controller is specified in the attributes section of the view page.

MVC diagram in CodeBehind Framework

![MVC diagram in CodeBehind Framework](https://github.com/elanatframework/Code_behind/assets/111444759/1def5400-6494-4458-af77-b44ea41a749d)
