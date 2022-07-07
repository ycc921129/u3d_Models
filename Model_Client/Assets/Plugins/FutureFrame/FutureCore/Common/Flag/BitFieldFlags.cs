/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

/********************************************
 * 
 * 枚举用法:添加特性[Flags]
 * 
 *********************************************/

using System;

namespace FutureCore
{
    /// <summary>
    /// 位域标志量
    /// </summary>
    public class BitFieldFlags
    {
        /// <summary>
        /// 位标识
        /// </summary>
        public long Value { get; set; }

        public BitFieldFlags() { }

        public BitFieldFlags(long flag)
        {
            Value = flag;
        }

        /// <summary>
        /// 添加一个标志量
        /// </summary>
        public long AddFlag(long flag)
        {
            Value |= flag;
            return Value;
        }

        /// <summary>
        /// 移除一个标志量
        /// </summary>
        public long RemoveFlag(long flag)
        {
            Value &= ~flag;
            return Value;
        }

        /// <summary>
        /// 添加或移除一个标志量
        /// </summary>
        public long ModifyFlag(bool isRemove, long flag)
        {
            Value = isRemove ? RemoveFlag(flag) : AddFlag(flag);
            return Value;
        }

        /// <summary>
        /// 某个标志量是否存在
        /// </summary>
        public bool HasFlag(long flag)
        {
            return ((Value & flag) != 0);
        }
    }
}

/*

【位域枚举使用例子】

///<summary>
// 权限枚举
///</summary>
[Flags] // 位域
public enum permission //注意加了[Flags] 特性后有三种写法，
{　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　 //一种是使用<<符号，
    Unknown = 0, // 也可以写成0x00或0　　　　　　　　　　　　　//第二种是0x01,　　　　　　　　　　　　　　　　
　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　//还有一种是直接写0，1，2，4，8....，
    Create = 1<<0, // 0x01或1　　　　　　　　　　　　　　　　　//一般来说是2的n次方来表示。　　
　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　//注：<<左操作符，表示对这个数进行移位。
    Read = 1<<1,  //0x02或2

    Update = 1<<2, //0x04或4

    Delete = 1<<3  //0x08或8
}

//1、给用户创建、读取，修改和删除的权限
var parmission = Permission.Create | parmission.Read | parmission.Update | parmission.Delete;

//2、去掉用户的修改和删除权限
parmission = parmission &~parmission.Update;
parmission = parmission &~parmission.Delete;

//3、给用户加上修改的权限
parmission = parmission | parmission.Update;

//4、判断用户是否有创建的权限
var isCreate = (parmission & parmission.Create) != 0;
//或者
var isCreate = (parmission & parmission.Create) == parmission.Create;
这时parmission枚举的值将变成0+1+4=5，它的ToSting()将变成“parmission.Create，
parmission.Read”,parmission.Update; 这里我们可以解释为什么第五个值Delete是8而不能成为5。
也就是说它的值不应该是前几项值的复合值。一个比较简单的方法
就是用2的n次方来依次位每一项赋值，例如：1，2，4，8，16，32，64.......。

*/