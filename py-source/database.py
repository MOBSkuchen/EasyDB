import os
import json
import shutil
from .exceptions import *


class DataBase:
    def __init__(self, name: str, root: str):
        self.name = name
        self.position = root + self.name
        if not os.path.exists(self.position):
            os.mkdir(self.position)

    @staticmethod
    def _document_exists(position: str):
        return os.path.exists(position)

    def document_exists(self, name: str):
        position = self.position + "/" + name + ".json"
        return os.path.exists(position)

    def doc_add(self, name: str):
        position = self.position + "/" + name + ".json"
        if self._document_exists(position):
            raise Exceptions.DocumentAlreadyExists("Document already exists in " + position)
        else:
            open(position, "w").write("{}")

    def get_raw(self, position: str):
        if self._document_exists(position):
            return open(position, "r").read()
        else:
            raise Exceptions.DocumentDoesNotExists("Document not found")

    def inject(self, name: str, key: str, value: object):
        position = self.position + "/" + name + ".json"
        if self._document_exists(position):
            load: dict = json.loads(self.get_raw(position))
            load[key] = value
            load2 = json.dumps(load)
            open(position, "w").write(str(load2))
            del position, load
        else:
            raise Exceptions.DocumentDoesNotExists("Document not found")

    def paste(self, name: str, dictionary: dict):
        position = self.position + "/" + name + ".json"
        if self._document_exists(position):
            load: str = json.dumps(dictionary)
            open(position, "w").write(str(load))
            del position, load
        else:
            raise Exceptions.DocumentDoesNotExists("Document not found")

    def remove(self, name: str, key: str):
        position = self.position + "/" + name + ".json"
        if self._document_exists(position):
            load: dict = json.loads(self.get_raw(position))
            load.pop(key)
            load2 = json.dumps(load)
            open(position, "w").write(str(load2))
            del position, load
        else:
            raise Exceptions.DocumentDoesNotExists("Document not found")

    def eject(self, name: str, key: str):
        position = self.position + "/" + name + ".json"
        if self._document_exists(position):
            load: dict = json.loads(self.get_raw(position))
            return load[key]
        else:
            raise Exceptions.DocumentDoesNotExists("Document not found")

    def delete(self):
        shutil.rmtree(self.position)

    def list_docs(self):
        return os.listdir(self.position)

    def doc_load(self, name: str):
        position = self.position + "/" + name + ".json"
        return json.loads(self.get_raw(position))

    def doc_del(self, name: str):
        position = self.position + "/" + name + ".json"
        if self._document_exists(position):
            os.remove(position)
        else:
            raise Exceptions.DocumentDoesNotExists("Document not found")


class Engine:
    def __init__(self, root: str):
        self.databases = []
        self.root: str = root
        for name in os.listdir(root):
            if not root == name:
                db: DataBase = DataBase(name, self.root)
                self.databases.append(db)

    def database_exists(self, name: str):
        for db in self.databases:
            if name == db.name:
                return True
        return False

    def add_database(self, name: str):
        db = DataBase(name, self.root)
        self.databases.append(db)

    def localize_database(self, name: str):
        index: int = -1
        for db in self.databases:
            index += 1
            if name == db.name:
                return db, index
        return None

    def remove_database(self, name: str):
        db, index = self.localize_database(name)
        if db is not None:
            shutil.rmtree(db.position)
            self.databases.remove(db)
        else:
            raise Exceptions.DataBaseDoesNotExists("DataBase not found")
