using OEMSharedFolderAccessLib;
using System;
using System.Collections.Immutable;
using Windows.UI;

class PersonalizationHelper
{
    public class ErrorMessaging
    {
        public Exception Exception { get; set; }
        public bool Result { get; set; }
    }

    public enum RegistryHives
    {
        HKEY_CLASSES_ROOT = 0,
        HKEY_CURRENT_MACHINE = 1,
        HKEY_CURRENT_USER = 2,
        HKEY_CURRENT_CONFIG = 5
    }

    public enum RegistryType
    {
        REG_SZ = 1,
        REG_BINARY = 3,
        REG_DWORD = 4,
        REG_MULTI_SZ = 7
    }

    public enum BackgroundAccents
    {
        Light = 0,
        Dark = 1
    }

    public enum EnabledEnum
    {
        Disabled = 0,
        Enabled = 1
    }

    public static COEMSharedFolder lrpc = new COEMSharedFolder();

    public static ErrorMessaging InitializeRegistryAccess()
    {
        try
        {
            if (lrpc.RPC_Init() == 0)
                return new ErrorMessaging() { Exception = null, Result = false };
            else
                return new ErrorMessaging() { Exception = null, Result = true };
        }
        catch (Exception ex)
        {
            return new ErrorMessaging() { Exception = ex, Result = false };
        }
    }

    public static ErrorMessaging SetRegistryValue(RegistryHives Hive, string Path, string Key, string Value, RegistryType Type)
    {
        try
        {
            lrpc.rset(Convert.ToUInt32(Hive), Path, Key, Convert.ToUInt32(Type), Value, 0);
            return new ErrorMessaging() { Exception = null, Result = true };
        }
        catch (Exception ex)
        {
            return new ErrorMessaging() { Exception = ex, Result = false };
        }
    }

    public static string GetRegistryValue(RegistryHives Hive, string Path, string Key, RegistryType Type)
    {
        try
        {
            return lrpc.rget(Convert.ToUInt32(Hive), Path, Key, Convert.ToUInt32(Type));
        }
        catch
        {
            return "";
        }
    }

    public static ErrorMessaging BackgroundAccentSetter(BackgroundAccents Color)
    {
        try
        {
            if (Color == BackgroundAccents.Dark)
            {
                var res = SetRegistryValue(RegistryHives.HKEY_CURRENT_MACHINE, @"Software\Microsoft\Windows\CurrentVersion\Control Panel\Theme", "CurrentTheme", "00000001", RegistryType.REG_DWORD);
                if (res.Exception != null)
                {
                    return new ErrorMessaging() { Exception = res.Exception, Result = false };
                }
                return new ErrorMessaging() { Exception = null, Result = res.Result };
            }
            else
            {
                var res = SetRegistryValue(RegistryHives.HKEY_CURRENT_MACHINE, @"Software\Microsoft\Windows\CurrentVersion\Control Panel\Theme", "CurrentTheme", "00000000", RegistryType.REG_DWORD);
                if (res.Exception != null)
                {
                    return new ErrorMessaging() { Exception = res.Exception, Result = false };
                }
                return new ErrorMessaging() { Exception = null, Result = res.Result };
            }
        }
        catch (Exception ex)
        {
            return new ErrorMessaging() { Exception = ex, Result = false };
        }
    }
    
    public static ErrorMessaging AppBackgroundAccentSetter(BackgroundAccents Color)
    {
        try
        {
            if (Color == BackgroundAccents.Dark)
            {
                SetRegistryValue(RegistryHives.HKEY_CURRENT_MACHINE, @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", "SystemUsesLightTheme", "00000000", RegistryType.REG_DWORD);
                var res = SetRegistryValue(RegistryHives.HKEY_CURRENT_MACHINE, @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", "00000000", RegistryType.REG_DWORD);
                if (res.Exception != null)
                {
                    return new ErrorMessaging() { Exception = res.Exception, Result = false };
                }
                return new ErrorMessaging() { Exception = null, Result = res.Result };
            }
            else
            {
                SetRegistryValue(RegistryHives.HKEY_CURRENT_MACHINE, @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", "SystemUsesLightTheme", "00000001", RegistryType.REG_DWORD);
                var res = SetRegistryValue(RegistryHives.HKEY_CURRENT_MACHINE, @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", "00000001", RegistryType.REG_DWORD);
                if (res.Exception != null)
                {
                    return new ErrorMessaging() { Exception = res.Exception, Result = false };
                }
                return new ErrorMessaging() { Exception = null, Result = res.Result };
            }
        }
        catch (Exception ex)
        {
            return new ErrorMessaging() { Exception = ex, Result = false };
        }
    }

    private static ErrorMessaging ChangePhoneAccent(string HexValue, string CurrentAccent)
    {
        try
        {
            for (int i = 0; i <= 1; i++)
            {
                SetRegistryValue(RegistryHives.HKEY_CURRENT_MACHINE, $@"Software\Microsoft\Windows\CurrentVersion\Control Panel\Theme\Themes\{i}\Accents\{CurrentAccent}", "Color", HexValue, RegistryType.REG_DWORD);
                SetRegistryValue(RegistryHives.HKEY_CURRENT_MACHINE, $@"Software\Microsoft\Windows\CurrentVersion\Control Panel\Theme\Themes\{i}\Accents\{CurrentAccent}", "ComplementaryColor", HexValue, RegistryType.REG_DWORD);
            }
            return new ErrorMessaging() { Exception = null, Result = true };
        }
        catch (Exception ex)
        {
            return new ErrorMessaging() { Exception = ex, Result = false };
        }
    }

    public static ErrorMessaging PhoneAccentSetter(byte Red, byte Blue, byte Green)
    {
        try
        {
            var col = Color.FromArgb(255, Red, Green, Blue);
            var CurrentAccentHex = GetRegistryValue(RegistryHives.HKEY_CURRENT_MACHINE, @"Software\Microsoft\Windows\CurrentVersion\Control Panel\Theme", "CurrentAccent", RegistryType.REG_DWORD);
            var CurrentAccent = int.Parse(CurrentAccentHex, System.Globalization.NumberStyles.HexNumber);
            var res = ChangePhoneAccent(col.ToString().Replace("#", string.Empty), CurrentAccent.ToString());
            var regvalue = GetRegistryValue(RegistryHives.HKEY_CURRENT_MACHINE, @"Software\Microsoft\Windows\CurrentVersion\Control Panel\Theme", "AccentPalette", RegistryType.REG_BINARY);
            var array = regvalue.ToCharArray();

            array.SetValue(col.ToString().Replace("#", string.Empty).ToCharArray().GetValue(0), 24);
            array.SetValue(col.ToString().Replace("#", string.Empty).ToCharArray().GetValue(1), 25);
            array.SetValue(col.ToString().Replace("#", string.Empty).ToCharArray().GetValue(2), 26);
            array.SetValue(col.ToString().Replace("#", string.Empty).ToCharArray().GetValue(3), 27);
            array.SetValue(col.ToString().Replace("#", string.Empty).ToCharArray().GetValue(4), 28);
            array.SetValue(col.ToString().Replace("#", string.Empty).ToCharArray().GetValue(5), 29);

            var newpalette = string.Join("", array);
            SetRegistryValue(RegistryHives.HKEY_CURRENT_MACHINE, @"Software\Microsoft\Windows\CurrentVersion\Control Panel\Theme", "AccentPalette", newpalette, RegistryType.REG_BINARY);
            var CurrentAccentstr = CurrentAccent.ToString();
            var len = CurrentAccentstr.Length;
            for (int i = 0; i < (8 - len); i++)
            {
                CurrentAccentstr = "0" + CurrentAccentstr;
            }
            if (CurrentAccent != 1)
            {
                SetRegistryValue(RegistryHives.HKEY_CURRENT_MACHINE, @"Software\Microsoft\Windows\CurrentVersion\Control Panel\Theme", "CurrentAccent","00000001", RegistryType.REG_DWORD);
                SetRegistryValue(RegistryHives.HKEY_CURRENT_MACHINE, @"Software\Microsoft\Windows\CurrentVersion\Control Panel\Theme", "CurrentAccent", CurrentAccentstr, RegistryType.REG_DWORD);
            }
            else
            {
                SetRegistryValue(RegistryHives.HKEY_CURRENT_MACHINE, @"Software\Microsoft\Windows\CurrentVersion\Control Panel\Theme", "CurrentAccent", "00000002", RegistryType.REG_DWORD);
                SetRegistryValue(RegistryHives.HKEY_CURRENT_MACHINE, @"Software\Microsoft\Windows\CurrentVersion\Control Panel\Theme", "CurrentAccent", CurrentAccentstr, RegistryType.REG_DWORD);
            }
            return new ErrorMessaging() { Exception = null, Result = res.Result };
        }
        catch (Exception ex)
        {
            return new ErrorMessaging() { Exception = ex, Result = false };
        }
    }

    public static ErrorMessaging HighContrastModeSetter(EnabledEnum IsEnabled)
    {
        try
        {
            if (IsEnabled == EnabledEnum.Disabled)
            {
                var res = SetRegistryValue(RegistryHives.HKEY_CURRENT_MACHINE, @"Software\Microsoft\Windows\CurrentVersion\Control Panel\Theme", "HighContrast", "00000000", RegistryType.REG_DWORD);
                if (res.Exception != null)
                {
                    return new ErrorMessaging() { Exception = res.Exception, Result = false };
                }
                return new ErrorMessaging() { Exception = null, Result = res.Result };
            }
            else
            {
                var res = SetRegistryValue(RegistryHives.HKEY_CURRENT_MACHINE, @"Software\Microsoft\Windows\CurrentVersion\Control Panel\Theme", "HighContrast", "00000001", RegistryType.REG_DWORD);
                if (res.Exception != null)
                {
                    return new ErrorMessaging() { Exception = res.Exception, Result = false };
                }
                return new ErrorMessaging() { Exception = null, Result = res.Result };
            }
        }
        catch (Exception ex)
        {
            return new ErrorMessaging() { Exception = ex, Result = false };
        }
    }
    
    public static ErrorMessaging FontScaleSetter(int Value)
    {
        try
        {
            var res = SetRegistryValue(RegistryHives.HKEY_CURRENT_MACHINE, @"Software\Microsoft\Windows\CurrentVersion\Control Panel\Theme", "FontScale", Value.ToString(), RegistryType.REG_DWORD);
            if (res.Exception != null)
            {
                return new ErrorMessaging() { Exception = res.Exception, Result = false };
            }
            return new ErrorMessaging() { Exception = null, Result = res.Result };
        }
        catch (Exception ex)
        {
            return new ErrorMessaging() { Exception = ex, Result = false };
        }
    }

   
   

   
    
    private static ImmutableList<Color> win10Colors = ImmutableList.Create(
        Color.FromArgb(0xff, 0xff, 0xb9, 0x00),
        Color.FromArgb(0xff, 0xff, 0x8c, 0x00),
        Color.FromArgb(0xff, 0xf7, 0x63, 0x0c),
        Color.FromArgb(0xff, 0xca, 0x50, 0x10),
        Color.FromArgb(0xff, 0xda, 0x3b, 0x01),
        Color.FromArgb(0xff, 0xef, 0x69, 0x50),
        Color.FromArgb(0xff, 0xd1, 0x34, 0x38),
        Color.FromArgb(0xff, 0xff, 0x43, 0x43),
        Color.FromArgb(0xff, 0xe7, 0x48, 0x56),
        Color.FromArgb(0xff, 0xe8, 0x11, 0x23),
        Color.FromArgb(0xff, 0xea, 0x00, 0x5e),
        Color.FromArgb(0xff, 0xc3, 0x00, 0x52),
        Color.FromArgb(0xff, 0xe3, 0x00, 0x8c),
        Color.FromArgb(0xff, 0xbf, 0x00, 0x77),
        Color.FromArgb(0xff, 0xc2, 0x39, 0xb3),
        Color.FromArgb(0xff, 0x9a, 0x00, 0x89),
        Color.FromArgb(0xff, 0x88, 0x17, 0x98),
        Color.FromArgb(0xff, 0xb1, 0x46, 0xc2),
        Color.FromArgb(0xff, 0x74, 0x4d, 0xa9),
        Color.FromArgb(0xff, 0x87, 0x64, 0xb8),
        Color.FromArgb(0xff, 0x6b, 0x69, 0xd6),
        Color.FromArgb(0xff, 0x8e, 0x8c, 0xd8),
        Color.FromArgb(0xff, 0x00, 0x63, 0xb1),
        Color.FromArgb(0xff, 0x00, 0x78, 0xd7),
        Color.FromArgb(0xff, 0x00, 0x99, 0xbc),
        Color.FromArgb(0xff, 0x2d, 0x7d, 0x9a),
        Color.FromArgb(0xff, 0x00, 0xb7, 0xc3),
        Color.FromArgb(0xff, 0x03, 0x83, 0x87),
        Color.FromArgb(0xff, 0x00, 0xb2, 0x94),
        Color.FromArgb(0xff, 0x01, 0x85, 0x74),
        Color.FromArgb(0xff, 0x00, 0xcc, 0x6a),
        Color.FromArgb(0xff, 0x10, 0x89, 0x3e),
        Color.FromArgb(0xff, 0x10, 0x7c, 0x10),
        Color.FromArgb(0xff, 0x49, 0x82, 0x05),
        Color.FromArgb(0xff, 0x48, 0x68, 0x60),
        Color.FromArgb(0xff, 0x56, 0x7c, 0x73),
        Color.FromArgb(0xff, 0x51, 0x5c, 0x6b),
        Color.FromArgb(0xff, 0x68, 0x76, 0x8a),
        Color.FromArgb(0xff, 0x5d, 0x5a, 0x58),
        Color.FromArgb(0xff, 0x7a, 0x75, 0x74),
        Color.FromArgb(0xff, 0x76, 0x76, 0x76),
        Color.FromArgb(0xff, 0x4c, 0x4a, 0x48),
        Color.FromArgb(0xff, 0x69, 0x79, 0x7e),
        Color.FromArgb(0xff, 0x4a, 0x54, 0x59),
        Color.FromArgb(0xff, 0x64, 0x7c, 0x64),
        Color.FromArgb(0xff, 0x52, 0x5e, 0x54),
        Color.FromArgb(0xff, 0x84, 0x75, 0x45),
        Color.FromArgb(0xff, 0x7e, 0x73, 0x5f)
    );
}
