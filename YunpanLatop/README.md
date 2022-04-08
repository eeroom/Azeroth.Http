## silverlight项目A关联web项目B
```
A项目的项目文件不做改动
B项目的项目文件进行修改，找到节点PropertyGroup，然后增加一条信息
<SilverlightApplicationList>{22E9E578-62FE-4CF0-83C7-6F27FA2B67A8}|..\YunpanLatop\YunpanLatop.csproj|ClientBin|False</SilverlightApplicationList>

关联后的效果就是：A项目编译后，会生成一个xap文件到B项目的ClientBin目录下，
这个本质是msbuild执行项目的编译target，某个target中有命令让msbuild执行了这个操作，具体哪个target待研究
```