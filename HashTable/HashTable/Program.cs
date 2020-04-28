using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            HashTable hash = new HashTable();
            hash.Add("187PM20569", "Nguyễn Thành Tân");
            hash.Add("187PM", "thanh tan");
            hash.Add("hello", "jfksf");
            hash.Add("jkfjsd", "jkfsdjf");
            Console.WriteLine(hash.Count);
            hash.Remove(":");
            Console.WriteLine(hash.Count);
            foreach (var item in hash.Keys)
                Console.WriteLine(item);

            #region Ví Dụ Về Collison
            //HashTableDemo HashDemo = new HashTableDemo();
            //HashDemo.Add(":", "Dấu 2 chấm");
            //HashDemo.Add("fsdfaf", "chuỗi fsdfaf");
            //Console.WriteLine("Vị Trí index sau khi xử lý của \":\" : [  {0}   ]" , HashDemo.GetHash(":"));
            //Console.WriteLine("Vị Trí index sau khi xử lý của fsdfaf : [  {0}   ]" , HashDemo.GetHash("fsdfaf"));
            //Console.WriteLine("Giá trị value của key \":\" là -> [   {0}   ]", HashDemo.getvalue(":"));
            #endregion
        }
    }

    #region Linked List
    class Node
    {
        public object key;
        public object value;
        public Node next;

        public Node(object key, object value)
        {
            this.key = key;
            this.value = value;
            next = null;
        }
    }
    #endregion

    class HashTable
    {
        Node[] arr;
        int size;
        public int Count;

        #region Contructor không tham số
        public HashTable()
        {
            this.size = 10000;
            arr = new Node[size];
            this.Count = 0;
        }
        #endregion

        #region Contructor có tham số size truyền vào
        public HashTable(int size)
        {
            arr = new Node[size];
            this.size = size;
            Count = 0;
        }
        #endregion

        #region Thuộc Tính [ Keys ] : lấy ra tất cả các Keys có trong Hashtable
        public object[] Keys { get => GetKeys().ToArray(); }
        public IEnumerable<object> GetKeys()
        {
            foreach (Node item in arr)
            {
                if (item == null)
                    continue;
                yield return item.key;
            }
        }
        #endregion

        #region Thuộc Tính [ Values ] : lấy ra tất cả các Values có trong Hashtable
        public object[] Values { get => GetValues().ToArray(); }
        public IEnumerable<object> GetValues()
        {
            foreach (var item in arr)
            {
                if (item == null)
                    continue;
                yield return item.value;
            }
        }
        #endregion      

        #region [ Add(object Key, object value) ] Thêm Key-Value vào HashTable
        public void Add(object Key, object value)
        {
            var index = GetHash(Key);                                 //Gán giá trị tính được từ hàm GetHash cho biến tạm Index                           
            Node hey = new Node(Key, value);                          //Khai báo một biến Node có tên là "hey"lưu giá trị key và value
            if (arr[index] == null)                                   //Kiểm tra tại vị trí thứ index trong mảng có đang bị trống         
            {
                arr[index] = hey;                                          // True => thực hiện gán arr[index] cho nó biến "hey" đã lưu
            }
            else                                                      // Sai           
            {
                Node hey2 = arr[index];                                    // Khai báo một Node lưu giá trị Node đã được lưu tại arr[index]
                while (hey2.next != null)                                  // Kiểm tra Node tiếp theo có null ko 
                {
                    hey2 = hey2.next;                                            // Nếu Vòng Lặp Đúng => chuyển qua node tiếp theo          
                }
                hey2.next = hey;                                           // 
            }
            Count++;
        }
        #endregion

        #region [ GetHash(object key) ] Chuyển Key thành Index trong array
        public int GetHash(object key)
        {
            var temp = key.ToString().ToCharArray();
            var result = 0;
            for (int i = 0; i < temp.Length; i++)
            {
                result += temp[i] * (i + 1);
            }
            return result % 46104728;
            //46104728
        }
        #endregion

        #region [ GetValue(object Key) ] Tìm Giá Trị Value Từ Key Đã Cho, Nếu Không Tìm Thấy Trả Về Null
        public object GetValue(object key)
        {
            var temp = GetHash(key);
            Node hey2 = arr[temp];
            if (hey2 != null)
            {
                while (hey2.key != key && hey2.next != null)
                {
                    hey2 = hey2.next;
                }
                return hey2.value;
            }

            return null;
        }
        #endregion      

        #region [ Remove() ] Xóa một Key-Value Trong HashTable
        public void Remove(object key)
        {
            var temp = GetHash(key);
            if (arr[temp] != null)
            {
                Node hey = arr[temp];
                if (hey.key == key)
                    arr[temp] = hey.next;
                else
                {
                    while (hey.next != null && hey.next != key && hey.next.next != null)
                    {
                        hey = hey.next;
                    }
                    hey.next = hey.next.next;
                }


                Count--;
            }
        }
        #endregion

        #region [ Clear() ] Xóa Toàn Bộ Key-Value Trong HashTable
        public void Clear()
        {
            arr = new Node[size];
            Count = 0;
        }
        #endregion       
    }
}
