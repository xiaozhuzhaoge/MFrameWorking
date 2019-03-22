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
    public class PlayerStateBaseWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(PlayerStateBase);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 9, 4);
			
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "LayerName", _g_get_LayerName);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "CurrentAniLength", _g_get_CurrentAniLength);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "CurrentLoopCount", _g_get_CurrentLoopCount);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "CurrentLoopProgress", _g_get_CurrentLoopProgress);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "CurrentNormalizedTime", _g_get_CurrentNormalizedTime);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "owner", _g_get_owner);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ani", _g_get_ani);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "cc", _g_get_cc);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "anistateName", _g_get_anistateName);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "owner", _s_set_owner);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "ani", _s_set_ani);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "cc", _s_set_cc);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "anistateName", _s_set_anistateName);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					PlayerStateBase gen_ret = new PlayerStateBase();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) >= 1 && (LuaTypes.LUA_TNONE == LuaAPI.lua_type(L, 2) || translator.Assignable<object>(L, 2)))
				{
					object[] _datas = translator.GetParams<object>(L, 2);
					
					PlayerStateBase gen_ret = new PlayerStateBase(_datas);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to PlayerStateBase constructor!");
            
        }
        
		
        
		
        
        
        
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LayerName(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStateBase gen_to_be_invoked = (PlayerStateBase)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.LayerName);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CurrentAniLength(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStateBase gen_to_be_invoked = (PlayerStateBase)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.CurrentAniLength);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CurrentLoopCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStateBase gen_to_be_invoked = (PlayerStateBase)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.CurrentLoopCount);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CurrentLoopProgress(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStateBase gen_to_be_invoked = (PlayerStateBase)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.CurrentLoopProgress);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CurrentNormalizedTime(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStateBase gen_to_be_invoked = (PlayerStateBase)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.CurrentNormalizedTime);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_owner(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStateBase gen_to_be_invoked = (PlayerStateBase)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.owner);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ani(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStateBase gen_to_be_invoked = (PlayerStateBase)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.ani);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_cc(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStateBase gen_to_be_invoked = (PlayerStateBase)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.cc);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_anistateName(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStateBase gen_to_be_invoked = (PlayerStateBase)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.anistateName);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_owner(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStateBase gen_to_be_invoked = (PlayerStateBase)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.owner = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ani(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStateBase gen_to_be_invoked = (PlayerStateBase)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.ani = (UnityEngine.Animator)translator.GetObject(L, 2, typeof(UnityEngine.Animator));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_cc(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStateBase gen_to_be_invoked = (PlayerStateBase)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.cc = (UnityEngine.CharacterController)translator.GetObject(L, 2, typeof(UnityEngine.CharacterController));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_anistateName(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                PlayerStateBase gen_to_be_invoked = (PlayerStateBase)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.anistateName = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
