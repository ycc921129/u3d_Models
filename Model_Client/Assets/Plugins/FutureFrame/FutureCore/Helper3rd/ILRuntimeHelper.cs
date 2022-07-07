/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

/*
 *  【CPU】
 *      1.动态更新DLL打Release版，打开DLL代码优化
 *      2.关闭调试模式的宏定义DISABLE_ILRUNTIME_DEBUG
 *
 *  【堆内存】
 *      1.移除PDB文件的加载
 *      2.控制热更DLL代码量
 *
 *  【语法】
 *      1.ref out + 数组、迭代器、等类型会导致 参数匹配出错。（2019.11.13存在）
 *      2.Params Enum[] 传参不被支持（2020.2.9）
 *      
 *  【反射篇】
 *      1.GetCustomAttribute
 *          因为这个接口被Clr重定向，所以可以看到，Get的CustomAttribute，实际上都是IL包装类型
 *          所以用这个接口获取hotfix中的类型，会返回所有hotfix中的Attribute。
 *      2.Attribute的构造函数 传参Typeof（T）
 *          如Test（typeof（T）），这个实际传进去类型会变成TypeRef类型，
 *          与实际的Type类型不同，需要自行绕过。
 *      3.Attribute
 *          需要用构造函数传参，不要直接用语法糖
 *
 *  【Hotfix注意事项】
 *      1.Hotfix多使用事件派发，避免爆栈
 *      2.Hotfix避免跨域继承
 *      3.Hotfix中不要写大量复杂的计算,特别是在Update之类的方法中
 *      4.Hotfix中调用Unity中的数据结构和API时,尽量不要调用泛型相关的,因为泛型会用到不同的数据类型
 *      5.Hotfix中继承Unity的类或接口,需要写Adaptor适配器
 *      6.Hotfix对多线程Thread不兼容,使用会导致Unity崩溃闪退
 *      7.Hotfix中重写Unity中的虚函数时,不能再调base.xxx(),否则会爆栈,也就是StackOverflow
 *      8.Hotfix中不支持unsafe,intptr,interup
 *      9.Unity调用Hotfix的方法,开销相对较大,尽量避免
 *      10.尽量使用强转，不用as运算符
 *      11.不使用Struct结构体
 *
 *  【Litjson】
 *      1.扩展类型解析
 *          JsonMapper.RegisterImporter和JsonMapper.RegisterExporter
 * 
 *  【注意事项】
 *      1.CLR绑定初始化要在最后注册
 *      2.直接使用字段，不要封装成属性
 *      3.不要使用结构体
 *      4.定义不要太长
 */

namespace FutureCore
{
    public static class ILRuntimeHelper
    {
    }
}