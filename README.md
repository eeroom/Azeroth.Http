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
## sqlservercompact搭配ef的codefirst模式,重建本地数据库
1. 手工操作
	```
	代码中关闭ef的数据库初始化策略：System.Data.Entity.Database.SetInitializer<HFDbContext>(null);
	使用工具或者代码创建数据sdf数据文件：compactview
	把项目设定为启动项目,vs的原因,ef迁移工具会从启动项目中读取数据库连接串!
	包管理控制台中把项目选为默认项目，然后执行数据迁移,更新到最新版本：Update-Database -Verbose
	适合线上变更场景,利用Update-Database -Script -SourceMigration: $InitialDatabase -TargetMigration:AddPostAbstract可以得到变更的sql语句
	```
2. 使用ef的数据库初始化能力
	```
	CreateDatabaseIfNotExists:默认策略，数据库不存在，生成数据库；一旦model发生变化，抛异常，提示走数据迁移。
		Database.SetInitializer<HFDbContext>(new System.Data.Entity.CreateDatabaseIfNotExists<HFDbContext>());
		这样等价上面的手工操作
	DropCreateDatabaseAlways：数据库每次都重新生成，仅适用于开发和测试场景
	DropCreateDatabaseIfModelChanges：一旦mode发送变化，删除数据库重新生成
	自定义策略,自己实现约定接口即可
	上述方式会删掉原有的旧数据，仅适合新部署或搭建新的本地环境，不适用线上环境变变更等场景，
	
	MigrateDatabaseToLatestVersion：自动数据迁移,程序起来后，会自动迁移数据库结构到最新的版本
	修改数据库初始化策略为：Database.SetInitializer<HFDbContext>(new MigrateDatabaseToLatestVersion<HFDbContext,Migrations.Configuration>());
	修改迁移配置类，
		在构造函数设置启用自动迁移：this.AutomaticMigrationsEnabled = true;
		启用允许修改表结构：this.AutomaticMigrationDataLossAllowed = true;
	非常适用于开发阶段场景：表结构和表数量经常变动，但是不丢失已有的一些假数据，我们只需要在修改表结构后，增加一个版本，重新启动程序即可
	```