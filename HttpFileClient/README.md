## silverlight项目A关联web项目B
```
A项目的项目文件不做改动
B项目的项目文件进行修改，找到节点PropertyGroup，然后增加一条信息
<SilverlightApplicationList>{22E9E578-62FE-4CF0-83C7-6F27FA2B67A8}|..\HttpFileClient\HttpFileClient.csproj|silverlight|False</SilverlightApplicationList>

关联后的效果就是：A项目编译后，会生成一个xap文件到B项目的silverlight目录下，
			或者是：B项目编译的时候，会去编译A项目并且得到xap文件放到silverlight目录下
这个本质是msbuild执行项目的编译target，某个target中有命令让msbuild执行了这个操作，具体哪个target待研究
```
## silverlight的开发环境搭建
```
必须开发机需要安装sdk和silverlight developer工具，在vs的安装镜像里面可以找到这两个，需要版本匹配
可选安装：微软提供的控件工具包，里面有很多好用的控件

客户机只需要安装运行时，并且版本越高越好

开发机避免安装运行时，应为sdk已经包含了，如果额外再安装运行时，然后运行时版本和开发工具不匹配，会导致无法调试
```