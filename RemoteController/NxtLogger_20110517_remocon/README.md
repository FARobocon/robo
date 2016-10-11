# nxtリモコンアプリケーション

### 動作条件
.Net Framework 3.5 以上

### 動作検証環境
Windows 7 Professional

### 開発環境
Visual Studio C# 2010以降

### 動作手順
1. PCとNXTのSPP(Bluetooth)接続を確立する（事前準備）。
2. ロボットを移動し、RUN→「BD_ADDR」が表示される画面まで進める。
3. NxtLoggerでSPP接続したポートを選択する。
4. NxtLoggerでCONNECTをクリックする。
5.CTSとDSRがアクティブになるのを待つ(グレーアウト→黒字に変わる)。
6. ボットは「RUN」を選択し、尻尾をおろすところまで進める。
7. NxtLoggerで「Mission」ボタンを押下する。Missionウィンドウが開く。
8. Missionウィンドウで、[w][a][s][z]キーと速度スライドバーを使ってロボットに命令を送る。  
 [w] 前進 [a] 左回転 [s] 右回転 [z] 後退
