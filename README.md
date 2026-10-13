Research 
1. Class vs Struct vs Record

Class: قالب بنعمل منه Objects، زي Student وCourse، وبيكون مناسب لما نحتاج بيانات ودوال وعلاقات بين الكائنات.
Struct: شبه الـ Class، بس نوعه Value Type، يعني لما تسند منه نسخة لمتغير تاني، القيمة بتتنسخ.
Record:شبه الـ Class، لكنه بيقارن بين الـ Objects على أساس البيانات اللي جواها بدل المرجع بشكل افتراضي.


2. What is an Indexer?

الـ Indexer في C# بيخلينا نوصل لبيانات الـ Object باستخدام [] زي الـ Array.

get: بتجيب القيمة.
set: بتغيّر القيمة.

مثال:

csharp
public string this[int index]
{
    get { return names[index]; }
    set { names[index] = value; }
}
