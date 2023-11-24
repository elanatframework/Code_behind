### Web part in CodeBehind

In CodeBehind, the physical executable pages (aspx) are placed in the root path, and this makes the program structured.

CodeBehind supports web parts; web parts are like other parts of the project and include aspx files.

![Web part in CodeBehind](https://github.com/elanatframework/Code_behind/assets/111444759/68a89f70-3a47-4170-8bb5-f844ea2beec2)

To add the web part in CodeBehind, just put the project files in the root.

In CodeBehind, you can run web parts that make changes to aspx files. You can edit all aspx files during project execution and responding to users.

In CodeBehind, the structure of web parts is the same as the structure of the main project; your main project includes aspx pages, dll files, and other client-side files (css, js, images, etc.); web parts in CodeBehind also include aspx pages, dll files and other client side files.

![Web part structer in CodeBehind](https://github.com/elanatframework/Code_behind/assets/111444759/6058b117-6d6c-4c54-8515-7c34efefb6c5)

The project created by using CodeBehind is automatically a modular project, that is, it has the ability to add web parts. In addition, each web part can be used in other projects.

The system built with CodeBehind is also a web part itself. Each web part can also be a separate system! The web part that adds the configuration of the Program.cs class is considered the main system.
