# ItcastOnline

>这是一个在线测试系统，可以进行在线统测，自我测试等基础功能。  

基于ASP.NET MVC（目标框架.NET Framework 4.5.2） + Entity Framework（版本：6.1.3）  
   
在线访问：[线上地址](http://zhangfive.cn)  

目前版本为 v1.2

## 预览页面

登录
![demo](https://github.com/Asylumrots/ItcastOnline/blob/master/Itcast.Webapp/Content/images/Login.png)

主页
![demo](https://github.com/Asylumrots/ItcastOnline/blob/master/Itcast.Webapp/Content/images/Index.png)

统测
![demo](https://github.com/Asylumrots/ItcastOnline/blob/master/Itcast.Webapp/Content/images/Test.png)

## 项目目录

``` 目录>
Itcast.BLL 逻辑控制  
Itcast.Common 工具类  
Itcast.DAL 数据访问  
Itcast.DALFactory > AbstractFactory.cs 通过抽象工厂封装了类的实例的创建，业务层可以通过会话层来直接获取操作类的实例，实现两层的连接耦合。  
Itcast.IBLL 逻辑控制接口  
Itcast.IDAL 数据访问接口  
Itcast.Model EF数据 
Itcast.Webapp 网站   
```

## 浏览器支持

常见浏览器 和 Internet Explorer 10+.

| [<img src="https://raw.githubusercontent.com/alrra/browser-logos/master/src/edge/edge_48x48.png" alt="IE / Edge" width="24px" height="24px" />](http://godban.github.io/browsers-support-badges/)</br>IE / Edge | [<img src="https://raw.githubusercontent.com/alrra/browser-logos/master/src/firefox/firefox_48x48.png" alt="Firefox" width="24px" height="24px" />](http://godban.github.io/browsers-support-badges/)</br>Firefox | [<img src="https://raw.githubusercontent.com/alrra/browser-logos/master/src/chrome/chrome_48x48.png" alt="Chrome" width="24px" height="24px" />](http://godban.github.io/browsers-support-badges/)</br>Chrome | [<img src="https://raw.githubusercontent.com/alrra/browser-logos/master/src/safari/safari_48x48.png" alt="Safari" width="24px" height="24px" />](http://godban.github.io/browsers-support-badges/)</br>Safari |
| --------- | --------- | --------- | --------- |
| IE10, IE11, Edge| last 2 versions| last 2 versions| last 2 versions

## 许可

[MIT license](https://github.com/Asylumrots/ItcastOnline)

Copyright (c) 2019-present Asylumrots
