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
客户端浏览器只需要装runtime即可,并且版本越高越好

开发机需要装：
	silverlight sdk(runtime+msbuild的编译配置)
	silverlight developer(vs集成,项目模板)
	silverlight toolkits(官方额外组件库)

特别说明:silverlight sdk已经包含runtime,不需要重复安装
特别注意:如果开发机装了runtime,并且版本和sdk中的runtime版本不一致,会导致无法调试
	所以开发机sdk版本低一点没关系,客户端的runtime版本高就行,这样客户端一定可以正常执行sdk打包出来的xap包
```