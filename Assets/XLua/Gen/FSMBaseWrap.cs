#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class FSMBaseWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(FSMBase);
			Utils.BeginObjectRegister(type, L, translator, 0, 22, 4, 4);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnDataAnalysis", _m_OnDataAnalysis);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnEnter", _m_OnEnter);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnExit", _m_OnExit);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnUpdate", _m_OnUpdate);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnFixedUpdate", _m_OnFixedUpdate);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnLatedUpdate", _m_OnLatedUpdate);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnTriggerEnter", _m_OnTriggerEnter);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnTriggerStay", _m_OnTriggerStay);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnTriggerExit", _m_OnTriggerExit);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnBecameInvisible", _m_OnBecameInvisible);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnBecameVisible", _m_OnBecameVisible);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnCollisionEnter", _m_OnCollisionEnter);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnCollisionStay", _m_OnCollisionStay);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnCollisionExit", _m_OnCollisionExit);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnDestroy", _m_OnDestroy);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnTransformChildrenChanged", _m_OnTransformChildrenChanged);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnTransformParentChanged", _m_OnTransformParentChanged);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnApplicationPause", _m_OnApplicationPause);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnEnable", _m_OnEnable);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnDisable", _m_OnDisable);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "MoveState", _m_MoveState);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetLuaTable", _m_GetLuaTable);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "Name", _g_get_Name);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "PreFixId", _g_get_PreFixId);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ownerMgr", _g_get_ownerMgr);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "scriptEnv", _g_get_scriptEnv);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "Name", _s_set_Name);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "PreFixId", _s_set_PreFixId);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "ownerMgr", _s_set_ownerMgr);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "scriptEnv", _s_set_scriptEnv);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "FSMBase does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnDataAnalysis(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FSMBase gen_to_be_invoked = (FSMBase)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    object[] _datas = translator.GetParams<object>(L, 2);
                    
                    gen_to_be_invoked.OnDataAnalysis( _datas );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnEnter(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FSMBase gen_to_be_invoked = (FSMBase)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    object[] _datas = translator.GetParams<object>(L, 2);
                    
                    gen_to_be_invoked.OnEnter( _datas );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnExit(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FSMBase gen_to_be_invoked = (FSMBase)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    object[] _datas = translator.GetParams<object>(L, 2);
                    
                    gen_to_be_invoked.OnExit( _datas );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnUpdate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FSMBase gen_to_be_invoked = (FSMBase)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.OnUpdate(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnFixedUpdate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FSMBase gen_to_be_invoked = (FSMBase)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.OnFixedUpdate(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnLatedUpdate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FSMBase gen_to_be_invoked = (FSMBase)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.OnLatedUpdate(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnTriggerEnter(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FSMBase gen_to_be_invoked = (FSMBase)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Collider _other = (UnityEngine.Collider)translator.GetObject(L, 2, typeof(UnityEngine.Collider));
                    
                    gen_to_be_invoked.OnTriggerEnter( _other );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnTriggerStay(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FSMBase gen_to_be_invoked = (FSMBase)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Collider _other = (UnityEngine.Collider)translator.GetObject(L, 2, typeof(UnityEngine.Collider));
                    
                    gen_to_be_invoked.OnTriggerStay( _other );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnTriggerExit(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FSMBase gen_to_be_invoked = (FSMBase)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Collider _other = (UnityEngine.Collider)translator.GetObject(L, 2, typeof(UnityEngine.Collider));
                    
                    gen_to_be_invoked.OnTriggerExit( _other );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnBecameInvisible(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FSMBase gen_to_be_invoked = (FSMBase)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.OnBecameInvisible(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnBecameVisible(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FSMBase gen_to_be_invoked = (FSMBase)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.OnBecameVisible(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnCollisionEnter(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FSMBase gen_to_be_invoked = (FSMBase)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Collision _other = (UnityEngine.Collision)translator.GetObject(L, 2, typeof(UnityEngine.Collision));
                    
                    gen_to_be_invoked.OnCollisionEnter( _other );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnCollisionStay(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FSMBase gen_to_be_invoked = (FSMBase)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Collision _other = (UnityEngine.Collision)translator.GetObject(L, 2, typeof(UnityEngine.Collision));
                    
                    gen_to_be_invoked.OnCollisionStay( _other );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnCollisionExit(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FSMBase gen_to_be_invoked = (FSMBase)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Collision _other = (UnityEngine.Collision)translator.GetObject(L, 2, typeof(UnityEngine.Collision));
                    
                    gen_to_be_invoked.OnCollisionExit( _other );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnDestroy(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FSMBase gen_to_be_invoked = (FSMBase)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.OnDestroy(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnTransformChildrenChanged(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FSMBase gen_to_be_invoked = (FSMBase)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.OnTransformChildrenChanged(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnTransformParentChanged(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FSMBase gen_to_be_invoked = (FSMBase)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.OnTransformParentChanged(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnApplicationPause(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FSMBase gen_to_be_invoked = (FSMBase)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _pauseStatus = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.OnApplicationPause( _pauseStatus );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnEnable(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FSMBase gen_to_be_invoked = (FSMBase)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.OnEnable(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnDisable(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FSMBase gen_to_be_invoked = (FSMBase)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.OnDisable(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_MoveState(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FSMBase gen_to_be_invoked = (FSMBase)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _stateName = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.MoveState( _stateName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetLuaTable(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FSMBase gen_to_be_invoked = (FSMBase)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _stateName = LuaAPI.lua_tostring(L, 2);
                    
                        XLua.LuaTable gen_ret = gen_to_be_invoked.GetLuaTable( _stateName );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Name(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FSMBase gen_to_be_invoked = (FSMBase)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.Name);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_PreFixId(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FSMBase gen_to_be_invoked = (FSMBase)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.PreFixId);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ownerMgr(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FSMBase gen_to_be_invoked = (FSMBase)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.ownerMgr);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_scriptEnv(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FSMBase gen_to_be_invoked = (FSMBase)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.scriptEnv);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Name(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FSMBase gen_to_be_invoked = (FSMBase)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.Name = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_PreFixId(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FSMBase gen_to_be_invoked = (FSMBase)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.PreFixId = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ownerMgr(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FSMBase gen_to_be_invoked = (FSMBase)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.ownerMgr = (FSMMgr)translator.GetObject(L, 2, typeof(FSMMgr));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_scriptEnv(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FSMBase gen_to_be_invoked = (FSMBase)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.scriptEnv = (XLua.LuaTable)translator.GetObject(L, 2, typeof(XLua.LuaTable));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
