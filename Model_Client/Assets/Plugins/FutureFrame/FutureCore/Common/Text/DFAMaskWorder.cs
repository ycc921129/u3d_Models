/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

using System.Collections.Generic;

namespace FutureCore
{
    public class DFANode
    {
        public char character;
        public int flag = 0; //标识 0：延续 1：终结
        public List<DFANode> nodes = new List<DFANode>();

        public DFANode(char character)
        {
            this.character = character;
        }

        public DFANode(char character, int flag)
        {
            this.character = character;
            this.flag = flag;
        }
    }

    public class DFAMaskWorder
    {
        private DFANode rootNode;

        public DFAMaskWorder(string[] content)
        {
            BuildTree(content);
        }

        /// <summary>
        /// 创建屏蔽字库
        /// </summary>
        private void BuildTree(string[] keyWords)
        {
            rootNode = new DFANode('R');
            for (int i = 0; i < keyWords.Length; i++)
            {
                string word = keyWords[i];
                char[] chars = word.ToCharArray();
                if (chars.Length > 0)
                {
                    Insert(rootNode, chars, 0);
                }
            }
        }

        /// <summary>
        /// 插入节点
        /// </summary>
        private void Insert(DFANode root, char[] characters, int index)
        {
            DFANode node = Find(root, characters[index]);
            if (node == null)
            {
                node = new DFANode(characters[index]);
                root.nodes.Add(node);
            }

            if (index == (characters.Length - 1))
                node.flag = 1;

            index++;
            if (index < characters.Length)
                Insert(node, characters, index);
        }

        /// <summary>
        /// 查找节点
        /// </summary>
        private DFANode Find(DFANode root, char character)
        {
            List<DFANode> nodes = root.nodes;
            DFANode result = null;
            for (int i = 0; i < nodes.Count; i++)
            {
                DFANode node = nodes[i];
                if (node.character == character)
                {
                    result = node;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// 是否通过屏蔽字库
        /// </summary>
        public bool Search(string content)
        {
            DFANode node = rootNode;
            int index = 0;
            char[] chars = content.ToCharArray();

            List<string> words = new List<string>();
            while (index < chars.Length)
            {
                node = Find(node, chars[index]);
                if (node == null)
                {
                    node = rootNode;
                    index = index - words.Count;
                    words.Clear();
                }
                else if (node.flag == 1)
                    return true;
                else
                    words.Add(chars[index].ToString());
                index++;
            }
            return false;
        }

        /// <summary>
        /// 获取可通过屏蔽字库的文本
        /// </summary>
        public string Replace(string content)
        {
            DFANode node = rootNode;
            int index = 0;
            char[] chars = content.ToCharArray();

            List<string> words = new List<string>();
            while (index < chars.Length)
            {
                node = Find(node, chars[index]);
                if (node == null)
                {
                    node = rootNode;
                    index = index - words.Count;
                    words.Clear();
                }
                else if (node.flag == 1)
                {
                    words.Add(chars[index].ToString());
                    index = index - words.Count + 1;
                    for (int i = index; i < index + words.Count; i++)
                        chars[i] = '*';

                    words.Clear();
                    node = rootNode;
                }
                else
                    words.Add(chars[index].ToString());
                index++;
            }
            return new string(chars);
        }
    }
}