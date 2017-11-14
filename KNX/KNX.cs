using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNX
{
    /**
 * KNX 标准数据类型 DPT 的长度关系
 * 
 * 1-Bit - (DPT 1.*) Boolean  0 - 1  Value display, Blinds, Switch, Slider switch, Snapper switch, Button, Media button, Scene button  
 * 4-Bit - (DPT 3.*) Controlled/Dimming  Up 0 - 7, Down 0 - 7  Snapper, Snapper switch, Button, Media button  
 * 8-Bit - (DPT 5.*) Unsigned Value  0 - 255  Value display, Slider, Slider switch, RGB color light, Button, Media button, Scene button  
 * 8-Bit - (DPT 6.*) Signed Value  -128 - 127  Value display, Slider, Slider switch, Button, Media button, Scene button  
 * 16-Bit - (DPT 7.*) Unsigned Value  0 - 65535  Value display, Slider, Slider switch, Button, Media button, Scene button  
 * 16-Bit - (DPT 8.*) Signed Value  -32768 - 32767  Value display, Slider, Slider switch, Button, Media button, Scene button  
 * 16-Bit - (DPT 9.*) Float Value   Value display, Slider, Slider switch, Button, Media button, Scene button  
 * 32-Bit - (DPT 12.*) Unsigned Value  0 - 4294967295  Value display, Button, Media button  
 * 32-Bit - (DPT 13.*) Signed Value  -2147483648 - 2147483647  Value display, Button, Media button  
 * 32-Bit - (DPT 14.*) Float Value   Value display, Button, Media button  
 * 14-Byte - (DPT 16.*) String Value   Value display, Button, Media button 
 */

    /*
     *  底层数据长度定义和枚举之间的关系
     *  
     *  { 1, 2, 3, 4, 5, 6, 7, 8, 16, 24, 32, 48, 64, 80, 112 }
     *  
     *  对应关系：
     *  Bit1 --> 1
     *  Bit2 --> 2
     *  Bit3 --> 3
     *  Bit4 --> 4
     *  Bit5 --> 5
     *  Bit6 --> 6
     *  Bit7 --> 7
     *  Bit8 --> 8
     *  Bit16 --> 16
     *  Bit24 --> 24
     *  Bit32 --> 32
     *  Bit48 --> 48
     *  Bit64 --> 64
     *  Bit80 --> 80
     *  Byte14 --> 112
     */
    public enum KNXDataType
    {
        Bit1,
        Bit2,
        Bit3,
        Bit4,
        Bit5,
        Bit6,
        Bit7,
        Bit8,
        Bit16,
        Bit24,
        Bit32,
        Bit48,
        Bit64,
        Bit80,
        Bit112,
        None

    }

    /*
     *  KNX 中开关的优先级和枚举的关系：
     *  System --> 0
     *  Normal --> 1
     *  Urgent --> 2
     *  Low --> 3
     * 
     */
    public enum KNXPriority
    {
        System = 0, Normal = 1, Urgent = 2, Low = 3
    }

}
