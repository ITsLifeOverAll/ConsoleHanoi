AsyncAwait 教學專案
==============

這個專案是 ITsLifeOverAll 的 [河內塔遊戲 (Game of Hanoi)](https://youtu.be/tu-LxIAaK1o) 
影片教學的原始程式碼。

使用 .Net 8, 您可以將它改為其它 .Net Core 3.1  以上的版本，應該都沒有問題。

User 每做一個動作，都會影響遊戲的狀態。主要的狀態有下列 6 種：
- ChooseSource
- ChooseTarget
- InvalidTarget
- Win
- Abort
- Restart

其中 Win, Abort, Restart 都是讓遊戲結束的狀態。

詳情請參考 Youtube [河內塔遊戲 (Game of Hanoi)](https://youtu.be/tu-LxIAaK1o) 影片中的說明。