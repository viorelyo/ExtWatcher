import pandas as pd
from sklearn import preprocessing

FEATURES = ['obj', 'endobj', 'stream', 'endstream', 'xref', 'trailer', 'startxref', 'Page', 'Encrypt',
            'ObjStm', 'JS', 'Javascript', 'AA', 'OpenAction', 'AcroForm', 'JBIG2Decode', 'RichMedia',
            'Launch', 'EmbeddedFile', 'XFA', 'Colors', 'Malicious']

df = pd.read_csv(r'C:\Users\viorel\Desktop\Bachelor\ML-Repos\Malicious_pdf_detection\pdfdataset.csv')
# print(df)
normalized_df = df[FEATURES[:-1]]
print(normalized_df.min())
print(normalized_df.max())
dataframe = (normalized_df - normalized_df.min()) / (normalized_df.max() - normalized_df.min())
mm = preprocessing.MinMaxScaler()
x_scaled = mm.fit_transform(x)
dataframe = pd.DataFrame(x_scaled)
print(FEATURES[-1])
dataframe["Class"] = df[FEATURES[-1]]
dataframe.to_csv('tst.csv', index=False)