# ItcastOnline

>这是一个在线测试系统，可以进行在线统测，自我测试等基础功能。  

基于ASP.NET MVC（目标框架.NET Framework 4.5.2） + Entity Framework（版本：6.1.3）  
    
* 登录密码MD5加密，随机生成验证码，邮件找回密码；  
* 利用Bootstarp,CanvasJS实现动态前端显示优化页面布局，提高美感度；   
* 使用Razor视图引擎进行页面数据显示和发送Ajax请求；  
* 使用EF6对后台数据进行数据库更新操作；  
* 使用自定义Fitter的过滤器进行权限校验；  
* 通过抽象工厂封装了类的实例的创建；  
* 数据会话层使用工厂模式，实现所有数据操作类的实例，业务层可以通过会话层来直接获取操作类的实例，实现两层的连接耦合。
    
目前版本为 v1.3.1

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

[MIT license](https://github.com/Asylumrots/ItcastOnline/blob/master/LICENSE)

Copyright (c) 2019-present Asylumrots
