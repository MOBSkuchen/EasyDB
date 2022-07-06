class Exceptions:
    class DataBaseAlreadyExists(BaseException):
        def __init__(self, *args, **kwargs):
            pass

    class DataBaseDoesNotExists(BaseException):
        def __init__(self, *args, **kwargs):
            pass

    class DocumentAlreadyExists(BaseException):
        def __init__(self, *args, **kwargs):
            pass

    class DocumentDoesNotExists(BaseException):
        def __init__(self, *args, **kwargs):
            pass
