import re

from Common.constants import QUERY_KEYWORDS


class QueryParser:
    def __init__(self):
        self.pattern = r'(\w*)\s*\=\s*([\w\~\@\#\$\%\&\(\)\=\+\-\:\\\/\,\.\!\?\'\"]*)'

    def parse(self, query):
        words = re.findall(self.pattern, query)
        if not words:
            return None

        # match only first appearance of keyword=param
        pair = words[0]
        keyword = pair[0].lower()
        param = pair[1]

        if keyword not in QUERY_KEYWORDS:
            return None
        if param == "":
            return None
        return {"keyword": keyword, "param": param}


