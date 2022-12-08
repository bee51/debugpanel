using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace DebugPanel.Scripts
{
    public class DebugFinder : MonoBehaviour
    {
        [SerializeField] private DebugPanelSlot debugPanelSlot;

        private Dictionary<(DebugPanelAttribute,string), (Type, MemberInfo)> _elementsDictionary =
            new Dictionary<(DebugPanelAttribute,string), (Type, MemberInfo)>();


        public void GetValues()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly assembly in assemblies)
            {
                Type[] types = assembly.GetTypes();
                foreach (var type in types)
                {
                    BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
                    MemberInfo[] members = type.GetMembers(flags);
                    foreach (var member in members)
                    {
                        if (member.CustomAttributes.ToArray().Length > 0)
                        {
                            IEnumerable<DebugPanelAttribute> attribute =
                                member.GetCustomAttributes<DebugPanelAttribute>();
                            if (attribute.Count() > 0)
                            {
                                foreach (var debugPanelAttribute in attribute)
                                {
                                    _elementsDictionary.Add((debugPanelAttribute,debugPanelAttribute.DisplayName), (type, member));
                                }

                                foreach (var debugPanelAttribute in _elementsDictionary.Keys)
                                {
                                    debugPanelSlot.CreateDictionary(debugPanelAttribute.Item1.DisplayName,
                                        debugPanelAttribute.Item1.DisplayValue);
                                }
                            }
                        }
                    }
                }
            }

            debugPanelSlot.SetButtons();
        }

        public void SetAttribute(string attributeName, string value)
        {
            foreach (var debugPanelAttribute in _elementsDictionary.Keys)
            {
                if (debugPanelAttribute.Item1.DisplayName == attributeName)
                {
                    var type = _elementsDictionary[debugPanelAttribute];
                    var findingObject = FindObjectOfType(type.Item1);
                    var directorProperty = type.Item1.GetField(type.Item2.Name);
                    if (debugPanelAttribute.Item1.IsString)
                    {
                        directorProperty.SetValue(findingObject, value);
                        debugPanelAttribute.Item1.DisplayValue = value;

                    }
                    else
                    {
                        var settedValue = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                        directorProperty.SetValue(findingObject, settedValue);
                        debugPanelAttribute.Item1.DisplayValue = settedValue.ToString();

                    }

                }
            }
        }


        #region OldAttributeFunction

        /*[ContextMenu("it is Setted")]
       public void SetAttributes(string attributeName,string value,bool isString)
       {
           Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

           foreach (Assembly assembly in assemblies)
           {
               Type[] types = assembly.GetTypes();
               foreach (var type in types)
               {
                   BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
                   MemberInfo[] members = type.GetMembers(flags);
                   foreach (var member in members)
                   {
                       if (member.CustomAttributes.ToArray().Length > 0)
                       {
                           IEnumerable<DebugPanelAttribute> attribute =
                               member.GetCustomAttributes<DebugPanelAttribute>();
                           if (attribute.Count() > 0)
                           {
                               List<DebugPanelAttribute> myList = attribute.ToList();
                               foreach (var debugPanelAttribute in myList)
                               {
                                   if (debugPanelAttribute.DisplayName==attributeName)
                                   {
                                       var findingObject = FindObjectOfType(type);
                                       var directorProperty = type.GetField(member.Name);
                                       if (isString)
                                       {
                                           directorProperty.SetValue(findingObject, value);
                                           
                                       }
                                       else
                                       {
                                           
                                       }
                                   }
                                
                               }
                           }
                       }
                   }
               }
           }*/
        /*}*/

        #endregion
    }
}