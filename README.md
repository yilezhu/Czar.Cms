# .NET Core实战项目之CMS 第一章 入门篇-开篇及总体规划

> 作者：依乐祝
> 原文地址：https://www.cnblogs.com/yilezhu/p/9977862.html 
## 写在前面

千呼万唤始出来，首先，请允许我长吸一口气！真没想到[一份来自28岁老程序员的自白](https://www.cnblogs.com/yilezhu/p/9966945.html) 这篇文章会这么火，更没想到的是张善友队长的公众号居然也转载了这篇文章，这就导致两天的时间就有两百多位读者朋友加入了.NET Core实战项目交流群（欢迎更多小伙伴进入交流.NET Core经验，QQ群号：637326624）！这让我顿感亚历山大！我自己的文笔有多差我是知道的，所以就有点担心写不好！同时我也得到了很多朋友的鼓励，所以我会很认真的来分享每一篇文章，希望能对大家入门.NET Core有所帮助！当然一个人的能力是有限的，如果我的文章中有出现错误的话，也希望大家能够帮我指正，这样才能更好地服务更多的后来者！
同时教程的编写我会采用敏捷开发的思想，先大致梳理下，后期会做持续更新的！这个系列我尽量每周三篇的速度来进行编写！

##  面向的对象
由于加群的大部分读者朋友都没怎么接触过.NET Core，甚至只是刚听说过.NET Core所以我会从最基础的概念开始写起，通过一个简单的CMS系统的实战项目，让你知其然更知其所以然！如果你是.NET Core的老鸟，那么这个系列的文章也会有你可以借鉴的地方！当然如果你觉得自己的能力足够强的话也可以看我们的另一个系列《[【.NET Core微服务实战-统一身份认证】开篇及目录索引](https://www.cnblogs.com/jackcao/p/9928879.html)》这个系列有一定的门槛，但却是国内不可多得的用.NET Core开发统一身份认证方面的系列文章。

## 篇章结构
这个篇章结构会随着系列教程的深入做相应的变化！请大家持续关注。

![](https://img2018.cnblogs.com/blog/1377250/201811/1377250-20181118143046957-1195059382.png)



### 入门篇
入门篇主要是带大家快速入门，并掌握.NET Core中最常用的概念为后面的开发篇做准备。只有掌握了这些知识你才算半只脚踏入了.NET Core的世界，掌握概念后再实际动手做的话你才能理解的更深刻，所以这里希望大家一定要跟着动手做，不要做眼高手低的人。
1. *[.NET Core实战项目之CMS 第一章 入门篇-开篇及目录索引](https://www.cnblogs.com/yilezhu/p/9977862.html)*
2. *[.NET Core实战项目之CMS 第二章 入门篇-快速入门ASP.NET Core看这篇就够了](https://www.cnblogs.com/yilezhu/p/9985451.html)*
3. *[.NET Core实战项目之CMS 第三章 入门篇-源码解析配置文件及依赖注入](https://www.cnblogs.com/yilezhu/p/9998021.html)*
4. *[.NET Core实战项目之CMS 第四章 入门篇-Git的快速入门及实战演练](https://www.cnblogs.com/yilezhu/p/10014027.html)*
5. *[.NET Core实战项目之CMS 第五章 入门篇-Dapper的快速入门看这篇就够了](https://www.cnblogs.com/yilezhu/p/10024091.html)*
6. *[.NET Core实战项目之CMS 第六章 入门篇-Vue的快速入门及其使用](https://www.cnblogs.com/yilezhu/p/10035275.html)*



### 设计篇
进行一个简单CMS系统的数据库逻辑结构的设计，不要跟我说什么Code First有多么先进，DB First多么Outer。在结果导向上我更习惯使用设计工具对整个系统设计后，再进行相关的开发。
1. *[.NET Core实战项目之CMS 第七章 设计篇-用户权限极简设计全过程](https://www.cnblogs.com/yilezhu/p/10056094.html)*
2. *[.NET Core实战项目之CMS 第八章 设计篇-内容管理极简设计全过程](https://www.cnblogs.com/yilezhu/p/10073642.html)*
3. *[.NET Core实战项目之CMS 第九章 设计篇-白话架构设计](https://www.cnblogs.com/yilezhu/p/10080136.html)*
4. *[.NET Core实战项目之CMS 第十章 设计篇-系统开发框架设计](https://www.cnblogs.com/yilezhu/p/10094357.html)*

### 开发篇
顾名思义，带着大家按照我们设计的数据库进行相关功能的开发！
待更新
### 测试篇
编写相应的测试用例，涉及单元测试，集成测试！
待更新
### 部署篇
对前面开发的系统进行Windows部署或者在Linux系统上进行部署。
待更新

## 开发工具
俗话说得好，工欲善其事必先利其器、巧妇难为无米之炊，，一款好的工具能够让你事半功倍！如果你连工具都懒得装的话，那么劝你右上角点击关闭按钮，离开本系列教程吧！暂时罗列如下，不定期更新。
### 代码编写工具
既然大家要进行.NET Core的开发，那么就强烈建议大家使用Visual Studio2017或者Visual Studio Code进行开发吧！VS2017的使用很简单，跟之前的几个版本的使用方式都大同小异，而Visual Studio Code的使用可能大家会比较陌生，好在有我的这篇《[使用Visual Studio Code开发.NET Core看这篇就够了](https://www.cnblogs.com/yilezhu/p/9926078.html)》文章可以教大家如何进行开发！
### 数据库工具
SqlServer2008R2及以上。当然系列文章演示的时候我会使用SqlServer进行演示。至于MySql以及Oracle的话大家也可以结合着教程修改下Sql语句即可。
### 数据库设计工具
Power Design、

### 源代码管理工具
git。现代开发如果你还不知道Git我想你真应该考虑下使用这个分布式的版本控制工具了！相比集中式的版本控制工具如SVN他有着与生俱来的诸多好处！


## 技术栈
.NET Core2.1+AutoFac+ FluentValidation +Dapper+Vue+Redis+SqlServer/Mysql

## GitHub开源地址

这个系列教程的源码我会开放在GitHub以及码云上，有兴趣的朋友可以下载查看！觉得不错的欢迎Star
GitHub：https://github.com/yilezhu/Czar.Cms
码云：https://gitee.com/yilezhu/Czar.Cms

如果你觉得这个系列对您有所帮助的话，欢迎以各种方式进行赞助，当然给个Star支持下也是可以滴！另外一种最简单粗暴的方式就是下面两种：

如果你觉得这个系列对您有所帮助的话，欢迎以各种方式进行赞助，当然给个Star支持下也是可以滴！另外一种最简单粗暴的方式就是下面两种或者关注我们的微信公众号：

![DotNetCore实战公众号](https://www.cnblogs.com/images/cnblogs_com/yilezhu/1359617/o_qrcode_for_gh_3d7593c84946_258.jpg)

![支付宝支付](https://img2018.cnblogs.com/blog/1377250/201812/1377250-20181205220251318-249891081.jpg)

![微信支付](https://img2018.cnblogs.com/blog/1377250/201812/1377250-20181205220301951-1597191961.png)
