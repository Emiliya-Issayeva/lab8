Лабораторная работа №8

Тема лабораторной работы: Реализация метода рекурсивного спуска для синтаксического анализа.

Цель работы: Разработать для грамматики алгоритм синтаксического анализа на основе метода рекурсивного спуска.

Задание: В соответствии с вариантом для заданной грамматики необходимо разработать и реализовать алгоритм синтаксического анализа на основе метода рекурсивного спуска.

В окне результатов отражается последовательность вызова процедур обработки символов грамматики в соответствии с деревом рекурсивного спуска. При наличии ошибки разбор продолжается с вышестоящего по отношению к ошибочному узлу куста.

![image](https://github.com/user-attachments/assets/70bc83aa-429f-4085-a41c-e7556bc49c0e)

Дополнительное задание:

Реализовать для своего варианта задания в данной лабораторной работе алгоритм лексического анализа (лексемная декомпозиция и поиск лексических ошибок).

Грамматика:

G[S]={Vt, Vn, P, S}

Vt={flight, passenger, trip, morning, is, prefers, like, need, depend, fly, non-stop, first, direct}

Vn={S, Noun phrase, Verb phrase, Adjective phrase, Noun, Verb, Adjective}

S - начальный символ.

P={
S → <Noun phrase> <Verb phrase>
<Noun phrase> → <Noun> ∣ <Adjective phrase> <Noun> ∣ ε
<Verb phrase> → <Verb> <Noun phrase>
<Adjective phrase> → <Adjective phrase> <Adjective> ∣ ε
<Noun> → flight | passenger | trip | morning
<Verb> → is | prefers | like | need | depend | fly
<Adjective> → non-stop | first | direct
}

Язык:

L(G[S])={(Adjective)∗ [Noun] Verb (Adjective)∗ [Noun], x∈Vt*}

Классификация грамматики (КС):

Согласно классификации Хомского, грамматика G[S] относится к контекстно-свободной грамматике.

Это определяется тем, что все её продукции имеют вид:

A → α,

где

A ∈ Vn - нетерминальный символ,

α ∈ V* - цепочка терминальных и/или нетерминальных символов,

ε - пустая строка.

Схема вызова функций:

Parse()                           // запуск анализа
└── ParseS()                      // разбор по правилу S → Noun phrase Verb phrase
    ├── ParseNounPhrase()        // левая часть
    │   └── ParseAdjectivePhrase()  // если есть прилагательные
    │       └── ParseAdjectivePhrase()  // рекурсивно, пока есть
    └── ParseVerbPhrase()        // правая часть
        ├── проверка Verb        // обязательно должен быть глагол
        └── ParseNounPhrase()    // разбор Noun phrase
            └── ParseAdjectivePhrase()     // если есть прилагательные
                └── ParseAdjectivePhrase()  // рекурсивно, пока есть

Тестовые примеры:

Правильный:

![image](https://github.com/user-attachments/assets/261a3b84-0406-44d1-b114-188ae66680c3)

Неправильный:

![image](https://github.com/user-attachments/assets/965cc967-2831-44b0-ae86-ff5e593eec49)

Диаграмма сканера:

![image](https://github.com/user-attachments/assets/79abdead-14a2-4d5a-92a2-9f4fbe60a190)
