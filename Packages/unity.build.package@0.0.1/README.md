#### ﻿菜单

###### Build

- Package

  生成 Unity Package



#### 配置文件

package.json

```
{
  "name": "com.xxx.yyy",
  "displayName": "name",
  "version": "0.0.1",
  "unity": "2018.3",
  "description": "description",
  "keywords": [
    "unity"
  ],
  "category": "Unity",
  "repoPackagePath": "../UnityPackages"
}
```



扩展成员

- repoPackagePath

  生成包的位置，相对于项目目录



[Unity Package Manager 文档](https://docs.unity3d.com/Packages/com.unity.package-manager-ui@1.8/manual/index.html)



#### 添加 [package].asmdef 文件

在 Package 文件夹右键菜单/Create/Assembly Definition

会将当前目录及所有的子目录的代码生成一个 .dll



Editor 目录下的代码需要另外创建一个 .asmdef

一般命名为 [package].Editor.asmdef



#### 设置 .asmdef 生成 .dll 的属性

首先复制一个空的 .dll ，命名与asmdef文件相同且在同一个目录位置(如：[package].asmdef.dll)，在生成包时会将 [package].asmdef.dll.meta 复制到生成 [package].dll.meta 



