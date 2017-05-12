using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugins_hierarchy.Class {
    public static class Model {
        public static List<Plugin> pluginList { get; set; } = new List<Plugin>();
        public static List<Object> typeList { get; set; } = new List<Object>();
        public static List<Object> objectList { get; set; } = new List<Object>();
        public static List<Int32> objectTypeList { get; set; } = new List<Int32>();
    }
}
