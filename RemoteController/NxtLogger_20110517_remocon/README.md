# nxtリモコンアプリケーション

### 動作条件
.Net Framework 3.5 以上

### 動作検証環境
Windows 7 Professional

### 開発環境
Visual Studio C# 2010以降

### 動作手順
* PCとNXTのSPP(Bluetooth)接続を確立する（事前準備）。
* ロボットを移動し、RUN→「BD_ADDR」が表示される画面まで進める。
* NxtLoggerでSPP接続したポートを選択する。
* NxtLoggerでCONNECTをクリックする。
* CTSとDSRがアクティブになるのを待つ(グレーアウト→黒字に変わる)。
* ロボットは「RUN」を選択し、尻尾をおろすところまで進める。
* NxtLoggerで「Mission」ボタンを押下する。Missionウィンドウが開く。
* Missionウィンドウで、[w][a][s][z]キーと速度スライドバーを使ってロボットに命令を送る。  
 [w] 前進 [a] 左回転 [s] 右回転 [z] 後退
