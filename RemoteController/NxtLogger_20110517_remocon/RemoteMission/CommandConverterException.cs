namespace RemoteMission
{
    using System;
    using System.Runtime.Serialization;

    public class CommandConverterException : Exception
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CommandConverterException()
        {

        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="s">任意の文字列</param>
        public CommandConverterException(string s)
            : base(s)
        {

        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="s">任意の文字列</param>
        /// <param name="e">内部で発生した例外</param>
        public CommandConverterException(string s, Exception e)
            : base(s, e)
        {

        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="info">シリアライズ情報</param>
        /// <param name="cxt">ストリームコンテキスト</param>
        public CommandConverterException(
            SerializationInfo info, StreamingContext cxt)
            : base(info, cxt)
        {

        }
    }
}
