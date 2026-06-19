/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：密码加密与验证帮助类（BCrypt + 旧 AES 兼容）              
*└──────────────────────────────────────────────────────────────┘
*/
using System;

namespace Czar.Cms.Core.Helper
{
    /// <summary>
    /// 密码帮助类：支持 BCrypt 新格式与旧 AES 格式的兼容验证。
    /// - 新密码使用 BCrypt Hash（不可逆）
    /// - 登录时自动识别格式并兼容验证
    /// - 验证通过后若为旧格式则升级为 BCrypt
    /// </summary>
    public static class PasswordHelper
    {
        /// <summary>
        /// BCrypt 工作因子（轮数），越大越安全但越慢，建议 10-12
        /// </summary>
        private const int WorkFactor = 11;

        /// <summary>
        /// 旧 AES 加密默认密钥
        /// </summary>
        private const string AesKey = "CzarCmsAesEncryptKeys"; // 与原 CzarCmsKeys.AesEncryptKeys 保持一致

        /// <summary>
        /// 判断存储的密码哈希是否为 BCrypt 格式（以 $2 开头）
        /// </summary>
        public static bool IsBCryptHash(string storedPassword)
        {
            return !string.IsNullOrEmpty(storedPassword) && storedPassword.StartsWith("$2");
        }

        /// <summary>
        /// 生成 BCrypt 哈希（不可逆）
        /// </summary>
        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));
            return BCrypt.Net.BCrypt.HashPassword(password, WorkFactor);
        }

        /// <summary>
        /// 验证密码（兼容旧 AES 格式和新的 BCrypt 格式）
        /// </summary>
        /// <param name="password">用户输入的明文密码</param>
        /// <param name="storedPassword">数据库中存储的密码（可能是 BCrypt 或 AES）</param>
        /// <param name="upgradeHash">验证成功后是否将旧格式升级为 BCrypt</param>
        /// <returns>验证结果</returns>
        public static PasswordVerifyResult Verify(string password, string storedPassword, bool upgradeHash = true)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(storedPassword))
                return new PasswordVerifyResult { IsValid = false, NeedsRehash = false };

            if (IsBCryptHash(storedPassword))
            {
                // BCrypt 格式：直接验证
                bool valid = BCrypt.Net.BCrypt.Verify(password, storedPassword);
                // 检查是否需要升级工作因子
                bool needsRehash = valid && NeedsRehash(storedPassword, WorkFactor);
                return new PasswordVerifyResult
                {
                    IsValid = valid,
                    NeedsRehash = needsRehash
                };
            }

            // 旧 AES 格式：尝试解密后比对
            string decrypted = AESEncryptHelper.Decode(storedPassword, AesKey);
            bool matches = string.Equals(decrypted, password, StringComparison.Ordinal);
            return new PasswordVerifyResult
            {
                IsValid = matches,
                NeedsRehash = matches && upgradeHash // 验证通过且需要升级
            };
        }

        /// <summary>
        /// 检查 BCrypt 哈希的工作因子是否低于指定值（需要升级）
        /// </summary>
        private static bool NeedsRehash(string bcryptHash, int currentWorkFactor)
        {
            try
            {
                var parts = bcryptHash.Split('$');
                if (parts.Length < 3) return false;
                var version = parts[1]; // 2a / 2b / 2y
                var costStr = parts[2];
                if (!int.TryParse(costStr, out int existingCost)) return false;
                return existingCost < currentWorkFactor;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// AES 加密（仅用于兼容旧数据初始化，不用于新密码存储）
        /// </summary>
        [Obsolete("仅用于旧数据迁移，新密码请使用 HashPassword")]
        public static string LegacyEncrypt(string password)
        {
            return AESEncryptHelper.Encode(password, AesKey);
        }
    }

    public class PasswordVerifyResult
    {
        public bool IsValid { get; set; }
        public bool NeedsRehash { get; set; }
    }
}
